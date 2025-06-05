using System.Linq;
using SqlToMySql.Data.SqlEntities;
using SqlToMySql.Data.SQLEntities;
using SqlToMySql.helpers;

namespace SqlToMySql.Implementations;

public class DapperSQL : IDapperSQL
{
    private readonly General _gen;
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    private readonly IMapper _map;
    private readonly ComposeCPB _cpb;
    private readonly ComposePatient _cp;

    procedure_info p;
    eusur_operative c;
    Queen_support queen_Support;
    private readonly UserManager<AppUser> _manager;
    private readonly RoleManager<AppRole> _roleManager;

    public DapperSQL(
        IConfiguration configuration,
        IHofuf hof,
        IMapper map,
        General gen,
        ComposePatient cp,
        UserManager<AppUser> manager,
        RoleManager<AppRole> roleManager,
        ComposeCPB cpb
    )
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _hof = hof;
        _map = map;
        _gen = gen;
        _manager = manager;
        _roleManager = roleManager;
        _cpb = cpb;
        _cp = cp;
    }

    public async Task<List<Operative>> GetListOfProcedures(int hospital_id)
    {
        if (await Is_mariadb_procedure_empty(hospital_id))
        {
            _ = new List<Operative>();
            _ = new List<Class_Procedure>();

            //var query ="select * from hofuf.dbo.operative o where o.SURGEON_NAME = 'M.P. Harder' or o.ASSISTANT_SURGEON = 'M.P. Harder'";
            string query = hospital_id switch
            {
                // Hofuf
                253 => "select * from hofuf.dbo.operative o where o.SURGEON_NAME = 'M.P. Harder' or o.ASSISTANT_SURGEON = 'M.P. Harder'",
                // kfafh
                34 => "select * from ecsur_kfafh.dbo.operative o where o.SURGEON_NAME = 'M.P. Harder' or o.ASSISTANT_SURGEON = 'M.P. Harder'",
                _ => throw new ArgumentException("Invalid hospital_id"),
            };
            using var connection = new SqlConnection(_connectionString);
            var documents = await connection.QueryAsync<Operative>(query);
            List<Operative> result = documents.ToList();
            foreach (Operative x in result)
            {
                await GetProceduresAsync(x);
            }

            return result;
        }
        return null;
    }

    private async Task<bool> Is_mariadb_procedure_empty(int hospital_id)
    {
        // Check if the procedures table is empty for the given hospital_id
        if (hospital_id == 253)
        {
            if((await _hof.GetListOfProcedures(hospital_id)).Count == 0) 
            {
                return true;
            }
        }
        else if (hospital_id == 34)
        {
             if((await _hof.GetListOfProcedures(hospital_id)).Count == 0) 
            {
                return true;
            }
        }
        // Add more hospitals as needed
        return false;
    }
    

    private async Task<int> GetProceduresAsync(Operative x)
    {
        Class_Procedure cp;
        Class_Preview_Operative_report pvo;
        var l = new List<string>();

        _ = new eusur_operative();
        eusur_operative h1 = await Eusur(x.PROCEDURE_ID);
        _ = new procedure_info();
        procedure_info h2 = await GetProcedure(x.PROCEDURE_ID);
        _ = new Queen_support();
        Queen_support h3 = await Get_Queen_support(x.PROCEDURE_ID);

        cp = new Class_Procedure
        {
            hospital = 253, // is code for hofuf
            Description = h2.fd_TYPE,
            fdType = h2.record_id,
            PatientId = (Int32)h2.PATIENT_ID,
            ProcedureId = x.PROCEDURE_ID,
            refPhys = TranslateCardiologist(h2.CARDIOLOGIST),
            SelectedSurgeon = TranslateEmployee(x.SURGEON_NAME),
            SelectedResponsibleSurgeon = TranslateEmployee(x.RESPONSIBLE_FOR_PROC),
            SelectedAnaesthesist = TranslateEmployee(h1.anaesthesist),
            SelectedPerfusionist = TranslateEmployee(h1.perfusionist),
            SelectedAssistant = TranslateEmployee(x.ASSISTANT_SURGEON),
            SelectedNurse1 = TranslateEmployee(h1.nurse_1),
            SelectedNurse2 = TranslateEmployee(h1.nurse_2),
            DateOfSurgery = h2.SURGERY_DATE,
            SelectedTiming = GetProcedureTiming(h1),
            SelectedUrgentTiming = x.STATUS_URGENT,
            SelectedEmergencyTiming = x.STATUS_URGENT,
            SelectedStartHr = h1.Skin_incision_start_hr,
            SelectedStartMin = h1.Skin_incision_start_min,
            SelectedStopHr = h1.Skin_incision_stop_hr,
            SelectedStopMin = h1.Skin_incision_stop_min,
            TotalTime = h1.Total_time,
            SelectedInotropes = Convert.ToInt32(h3.INOTR),
            SelectedPacemaker = Convert.ToInt32(h3.PACEMAKER),
            SelectedPleura = Convert.ToInt32(h3.PLEURA),
            Comment1 = h3.COMMENT_A,
            Comment2 = h3.COMMENT_B,
            Comment3 = h3.COMMENT_C,
        };
        await _hof.AddProcedure(cp);

        pvo = new Class_Preview_Operative_report
        {
            Id = 0,
            procedure_id = x.PROCEDURE_ID,
            regel_1 = "",
            regel_2 = "",
            regel_3 = "",
            regel_4 = "",
            regel_5 = "",
            regel_6 = "",
            regel_7 = "",
            regel_8 = "",
            regel_9 = "",
            regel_10 = "",
            regel_11 = "",
            regel_12 = "",
            regel_13 = ""
        };

        if (h1.free_text.Length > 0)
        {
            foreach (string line in GetSpreadOutFreeText(75, h1.free_text))
            {
                l.Add(line);
                switch (l.Count)
                {
                    case 1:
                        pvo.regel_1 = l[0];
                        break;
                    case 2:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        break;
                    case 3:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        break;
                    case 4:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        break;
                    case 5:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        break;
                    case 6:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        break;
                    case 7:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        break;
                    case 8:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        break;
                    case 9:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        pvo.regel_9 = l[8];
                        break;
                    case 10:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        pvo.regel_9 = l[8];
                        pvo.regel_10 = l[9];
                        break;

                    case 11:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        pvo.regel_9 = l[8];
                        pvo.regel_10 = l[9];
                        pvo.regel_11 = l[10];
                        break;

                    case 12:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        pvo.regel_9 = l[8];
                        pvo.regel_10 = l[9];
                        pvo.regel_11 = l[10];
                        pvo.regel_12 = l[11];
                        break;

                    case 13:
                        pvo.regel_1 = l[0];
                        pvo.regel_2 = l[1];
                        pvo.regel_3 = l[2];
                        pvo.regel_4 = l[3];
                        pvo.regel_5 = l[4];
                        pvo.regel_6 = l[5];
                        pvo.regel_7 = l[6];
                        pvo.regel_8 = l[7];
                        pvo.regel_9 = l[8];
                        pvo.regel_10 = l[9];
                        pvo.regel_11 = l[10];
                        pvo.regel_12 = l[11];
                        pvo.regel_13 = l[12];
                        break;
                }
            }

            await _hof.AddPreviewOpReport(pvo);
        }

        //  await _cpb.AddCPBAsync(x.PROCEDURE_ID);
        //  await _cp.AddPatientAsync(x.PROCEDURE_ID, (int)h2.PATIENT_ID, h2.record_id);
        //  await AddCabg(x.PROCEDURE_ID);
        // await AddValve(x.PROCEDURE_ID);
        //  await AddMinInv(x);

        return 1;
    }

    private static IEnumerable<string> GetSpreadOutFreeText(int maxLineLength, string sentence)
    {
        var words = sentence.Split(' ');
        var currentLine = new List<string>();
        var currentLength = 0;

        foreach (var word in words)
        {
            if (currentLength + word.Length + 1 > maxLineLength)
            {
                yield return string.Join(" ", currentLine);
                currentLine.Clear();
                currentLength = 0;
            }

            currentLine.Add(word);
            currentLength += word.Length + 1; // Account for space
        }

        if (currentLine.Count > 0)
        {
            yield return string.Join(" ", currentLine);
        }
    }

    private void checkLength(int v, string free_text)
    {
        throw new NotImplementedException();
    }

    private int TranslateEmployee(string test)
    {
        int help = Convert.ToInt32(_gen.GetEmployeeId(test));
        return help;
    }

    private static int GetProcedureTiming(eusur_operative op)
    {
        var help = 0;
        if (op.status_el == "1")
        {
            help = 1;
        }
        if (op.status_ur == "1")
        {
            help = 2;
        }
        if (op.status_em == "1")
        {
            help = 3;
        }
        if (op.status_salvage == "1")
        {
            help = 4;
        }
        return help;
    }

    private int TranslateCardiologist(string test)
    {
        int help = Convert.ToInt32(_gen.GetRefPhysId(test));
        return help;
    }

    private async Task<int> AddMinInv(Operative x)
    {
        Class_minInv cmin;
        _ = new Class_minInv { };
        cmin = _map.Map<Class_minInv>(x);
        await _hof.AddMinInv(cmin);
        return 1;
    }

    private async Task<int> AddCabg(int procedureId)
    {
        Class_CABG ca;
        var query4 = "select * from eusur_cabg o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<eusur_cabg>(query4, new { id = procedureId });

        if (result != null)
        {
            ca = new Class_CABG { };
            ca = _map.Map<Class_CABG>(result.First());
            await _hof.AddCabg(ca);
        }

        return 1;
    }

    private async Task<int> AddValve(int procedureId)
    {
        Class_Valve va;
        var query4 = "select * from valves o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Valves>(query4, new { id = procedureId });

        if (result != null)
        {
            va = new Class_Valve { };
            va = _map.Map<Class_Valve>(result.First());
            await _hof.AddValve(va);
        }

        return 1;
    }

    private async Task<procedure_info> GetProcedure(int Procedureid)
    {
        //get procedure_info
        var query2 = "Select * FROM dbo.procedure_info where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_procedure_info = await connection2.QueryAsync<procedure_info>(
            query2,
            new { id = Procedureid }
        );
        this.p = selected_procedure_info.First();
        return this.p;
    }

    private async Task<eusur_operative> Eusur(int Procedureid)
    {
        //get eusur_operative
        var query2 = "Select * FROM dbo.eusur_operative where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<eusur_operative>(
            query2,
            new { id = Procedureid }
        );
        this.c = selected_op.First();
        return this.c;
    }

    private async Task<Queen_support> Get_Queen_support(int ProcedureId)
    {
        //get eusur_operative
        var query2 = "Select * FROM dbo.queen_support where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<Queen_support>(
            query2,
            new { id = ProcedureId }
        );
        this.queen_Support = selected_op.First();
        return this.queen_Support;
    }

    public async Task CheckSurgeons(int hospital_id)
    {
        if (await _hof.GetListOfUsers(hospital_id) == 0)
        {
            // check if the roles are filled
            if (RolesAreFilled()) { }
            else
            {
                // Create the "Surgeon" role if it doesn't exist
                var role = new AppRole { Name = "Surgeon" };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    // Handle the error, e.g., log it or throw an exception
                    throw new Exception(
                        $"Failed to create role 'Surgeon': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}"
                    );
                }
            }
            // Add the surgeons from the xml file to the database
            var surgeons = _gen.GetSurgeonsFromXml(hospital_id);
            foreach (var surgeon in surgeons)
            {
                // check if this username already exists, because this person worked in multiple hospitals
                surgeon.UserName = surgeon.UserName.Trim(); // Ensure no leading/trailing spaces
                var existingUser = await _manager.FindByNameAsync(surgeon.UserName);
                if (existingUser != null)
                {
                    // add this hospital_id to worked_in
                    existingUser.worked_in += $",{surgeon.hospital_id}";
                }
                else
                {
                    surgeon.hospital_id = hospital_id; // Set hospital_id
                    surgeon.UserName = surgeon.UserName.Trim(); // Ensure no leading/trailing spaces
                    surgeon.Created = DateTime.Now; // Set Created date
                    surgeon.LastActive = DateTime.Now; // Set LastActive date
                    surgeon.active = true; // Set active status
                    surgeon.ltk = false; // Set ltk status
                    surgeon.PasswordSalt = Array.Empty<byte>(); // Initialize PasswordSalt
                    surgeon.PhotoUrl = ""; // Set PhotoUrl to empty string

                    var result = await _manager.CreateAsync(surgeon, "Pa$$w0rd"); // Use a default password
                    if (!result.Succeeded)
                    {
                        // Handle the error, e.g., log it or throw an exception
                        throw new Exception(
                            $"Failed to create surgeon {surgeon.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                        );
                    }

                    var roleresult = await _manager.AddToRoleAsync(surgeon, "Surgeon");
                    if (!roleresult.Succeeded)
                    {
                        // Handle the error, e.g., log it or throw an exception
                        throw new Exception(
                            $"Failed to add surgeon {surgeon.UserName} to role: {string.Join(", ", roleresult.Errors.Select(e => e.Description))}"
                        );
                    }
                }
            }
        }
    }

    private bool RolesAreFilled()
    {
        return _roleManager.Roles.Any();
    }

    public async Task CheckEmployeesAsync(int hospital_id)
    {
        if (await _hof.GetListOfEmployees(hospital_id) == 0)
        {
            // Add the surgeons from the xml file to the database
            var employees = _gen.GetEmployeesFromXml(hospital_id);
            foreach (var emp in employees)
            {
                await _hof.AddEmployee(emp);
            }
        }
    }
}
