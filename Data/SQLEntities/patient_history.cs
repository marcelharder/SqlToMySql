namespace SqlToMySql.Data.SqlEntities
{
    public class patient_history
    {
    [Key]
    public string PATIENT_ID {get; set;}
    public string cookie_String {get; set;}
    public string riskfactor_String{get; set;}
    public float WEIGHT {get; set;}
    public string WEIGHT_UNITS {get; set;} 
    public float HEIGHT {get; set;}
    public string HEIGHT_UNITS {get; set;}  
    public string RISK_FACTORS {get; set;}
    public string RF_SMOKING {get; set;}
    public string RF_SMOKING_CURRENT {get; set;}
    public int RF_SMOKING_CURRENT_PK {get; set;}    
    public string RF_CAD {get; set;}
    public string RF_DIABETES {get; set;}
    public string RF_DIABETES_CONTROL {get; set;}
    public string RF_MORBID_OBESITY {get; set;}
    public string RF_HYPERCHOLESTEROLEMIA {get; set;}
    public int RF_HYPERCHOLESTEROLEMIA_LEVEL {get; set;}    
    public string RF_RENAL_FAILURE {get; set;}
    public string RF_RENAL_FAIL_LAST {get; set;}
    public string RF_RENAL_FAILURE_DIALYSIS {get; set;}
    public string RF_HYPERTENSION {get; set;}
    public string RF_CEREBROVASCULAR_ACCIDENT {get; set;}
    public int RF_CEREBROVASCULAR_ACCIDENT_TIME {get; set;}      
    public string RF_INFECTIOUS_ENDOCARDITIS {get; set;}
    public int RF_INFECTIOUS_ENDOCARD_TYPE {get; set;}   
    public string RF_IMMUNOSUPPRESSIVE_RX {get; set;}
    public string RF_CHRONIC_LUNG_DIS {get; set;}
    public string RF_PVD {get; set;}
    public string RF_CVD {get; set;}
    public int RF_CVD_TYPE {get; set;}       
    public string CS_CARDIOMEGALY {get; set;}
    public string CS_MI {get; set;}
    public int CS_MI_TYPE {get; set;}     
    public string CS_MI_WHEN {get; set;}
    public string CS_CHF {get; set;}
    public string CS_ANGINA {get; set;}
    public int CS_ANGINA_TYPE {get; set;}       
    public string CS_ANGINA_UNSTAB_TYPE {get; set;}
    public string CS_CARDIOGENIC_SHOCK {get; set;}
    public int CS_CARDIO_SHOCK_TYPE {get; set;}      
    public string CS_RESUSCITATION {get; set;}
    public string CS_ARRHYTHMIAp {get; set;}
    public int CS_ARRHYTHMIA_TYPE {get; set;}        
    public string CS_ARR_VENT {get; set;}
    public string CS_ARR_AV_BLOCK {get; set;}
    public string CS_ARR_ATRIAL_FIB {get; set;}
    public string CS_ARR_CHB {get; set;}
    public string CS_ARR_ACUTE {get; set;}
    public string CS_ARR_CHRONIC {get; set;}
    public string CS_CLASS_CCS {get; set;}
    public int CS_CLASSIFICATION_NYHA {get; set;}        
    public int CV_INTERVENTION {get; set;}        
    public DateTime CV_DATE {get; set;}    
    public DateTime SURG_DATE {get; set;}   
    public string CV_OPS_WITH_BYPASS {get; set;}
    public string CV_OPS_WITHOUT_BYPASS {get; set;}
    public string CV_PREVIOUS_INTERVAL {get; set;}
    public string CV_CAB {get; set;}
    public string CV_VALVE {get; set;}
    public string CV_VALVE_REPLACE {get; set;}
    public string CV_VALVE_REPLACE_A {get; set;}
    public string CV_VALVE_REPLACE_M {get; set;}
    public string CV_VALVE_REPLACE_T {get; set;}
    public string CV_VALVE_REPLACE_P {get; set;}
    public string CV_VALVE_REPAIR {get; set;}
    public string CV_VALVE_REPAIR_A {get; set;}
    public string CV_VALVE_REPAIR_M {get; set;}
    public string CV_VALVE_REPAIR_T {get; set;}  
    public string CV_VALVE_REPAIR_P {get; set;}
    public string CV_INVASIVE_CABG {get; set;}
    public string CV_INVASIVE_VALVE {get; set;}
    public string CV_INVASIVE_VALVE_A {get; set;}
    public string CV_INVASIVE_VALVE_M {get; set;}
    public string CV_INVASIVE_VALVE_T {get; set;}
    public string CV_INVASIVE_VALVE_P {get; set;}
    public string CV_OTHER_CARDIAC {get; set;}
    public string CV_OTHER_CARDIAC_LVA {get; set;}
    public string CV_OTHER_CARDIAC_VSD {get; set;}
    public string CV_OTHER_CARDIAC_ASD {get; set;} 
    public string CV_OTHER_CARDIAC_CONGENITAL {get; set;}
    public string CV_OTHER_CARDIAC_TRAUMA {get; set;} 
    public string CV_OTHER_CARDIAC_BATISTA {get; set;}
    public string CV_OTHER_CARDIAC_TX {get; set;}
    public string CV_OTHER_CARDIAC_PACEMAKER {get; set;}
    public string CV_OTHER_CARDIAC_AICD {get; set;}
    public string CV_OTHER_CARDIAC_OTHER {get; set;} 
    public string CV_OTHER_NONCARDIAC {get; set;}
    public string CV_OTHER_NONCARDIAC_ANEURYSM {get; set;} 
    public string CV_OTHER_NONCARDIAC_ASC {get; set;}
    public string CV_OTHER_NONCARDIAC_ARCH {get; set;} 
    public string CV_OTHER_NONCARDIAC_DESC {get; set;}
    public string CV_OTHER_NONCARDIAC_THOR {get; set;}
    public string CV_OTHER_NONCARDIAC_ABD {get; set;}
    public string CV_OTHER_NONCARDIAC_ENDART {get; set;}
    public string CV_OTHER_NONCARDIAC_OTHER_VASC {get; set;} 
    public string CV_OTHER_NONCARDIAC_OTHER_THORACIC {get; set;}
    public string CV_THROMBO {get; set;}
    public string CV_THROMBO_INTERVAL {get; set;}
    public int CV_THROMBO_AGENT {get; set;}       
    public string CV_PTCA_ATHER {get; set;}
    public string CV_PTCA_STATUS {get; set;}
    public int CV_PTCA_INTERVAL {get; set;}         
    public string CV_PTCA_SURG_SAME_ADM {get; set;}
    public string CV_PTCA_NUM_PRIOR_PTCA {get; set;}
    public string CV_NONSURG {get; set;}
    public string CV_NONSURG_PTCA {get; set;}
    public string CV_NONSURG_LASER {get; set;}
    public string CV_NONSURG_STENT {get; set;}
    public string CV_NONSURG_THROMBOLYSIS {get; set;}
    public string CV_NONSURG_BALLOON {get; set;}
    public string CV_NONSURG_BALLOON_A {get; set;}
    public string CV_NONSURG_BALLOON_M {get; set;}
    public string CV_NONSURG_BALLOON_T {get; set;}
    public string CV_NONSURG_BALLOON_P {get; set;}
    public int CV_PREOP_MEDS {get; set;}       
    public int CV_PREOP_MEDS_DIGITALIS {get; set;}        
    public int CV_PREOP_MEDS_BETA {get; set;}         
    public int CV_PREOP_MEDS_CA {get; set;}         
    public int CV_PREOP_MEDS_ACE {get; set;}         
    public int CV_PREOP_MEDS_NITRATES_PO {get; set;}         
    public int CV_PREOP_MEDS_NITRATES_IV {get; set;}         
    public int CV_PREOP_MEDS_ANTIARRHYTHMICS {get; set;}         
    public int CV_PREOP_MEDS_ANTIPLATELETS {get; set;}         
    public int CV_PREOP_MEDS_ANTICOAGULANTS {get; set;}         
    public int CV_PREOP_MEDS_DIURETICS {get; set;}         
    public int CV_PREOP_MEDS_INOTROPES {get; set;}         
    public int CV_PREOP_MEDS_STEROIDS {get; set;}          
    public int CV_PREOP_MEDS_ASPIRIN {get; set;}         
    public string DX_CAD {get; set;}
    public string DX_CONGENITAL {get; set;}
    public string DX_VALVULAR {get; set;}
    public string DX_AORTIC {get; set;}
    public string DX_IHSS {get; set;}
    public string DX_LVA {get; set;} 
    public float STENT_INTVL {get; set;}  
   


    }
}