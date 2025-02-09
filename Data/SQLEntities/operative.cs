namespace SqlToMySql.Data.SqlEntities;

public class Operative
{
    [Key]
    public int PROCEDURE_ID {get; set;}
    public int PATIENT_ID {get; set;}
    public string SURGEON_NAME {get; set;}
    public string SURGEON_GROUP {get; set;}
    public string ASSISTANT_SURGEON {get; set;}
    public string ass_no_2 {get; set;}
    public string RESIDENT {get; set;}
    public string RESPONSIBLE_FOR_PROC {get; set;}
    public int CLASSIFICATION_ASA {get; set;}
    public int REOP_REASON {get; set;}
    public string fd_STATUS {get; set;}
    public int STATUS_URGENT {get; set;}     
    public int STATUS_EMERGENT {get; set;}
    public int OP_CATEGORY {get; set;}
    public int PRED_RISK_OF_MORTALITY {get; set;}
    public int MINIMALLY_INVASIVE_PROCEDURE {get; set;}
    public int PRIM_IND_MIN_INVAS {get; set;}
    public int PRIMARY_INCISION {get; set;}
    public int TOTAL_INCISION_NUMBER {get; set;}
    public string CONVERSION_TO_STANDARD_INCISION {get; set;}
    public int IND_CON {get; set;}
    public int CARDIOPULMONARY_BYPASS {get; set;}
    public int AORTIC_OCCLUSION {get; set;}
    public int CANNULATION_METHOD {get; set;}
    public int INTRACORONARY_SHUNT {get; set;}
    public string ISCHEMIC_TIME_LAD {get; set;}
    public string ISCHEMIC_TIME_RCA {get; set;}
    public string ISCHEMIC_TIME_CFX {get; set;}
    public int SUTURE_TECHNIQUE {get; set;}
    public int VESSEL_STABILIZATION {get; set;}
    public int IMA_HARVEST_TECH {get; set;}
    public int FLOW_CHECK {get; set;}
    public string OTHER_CARD {get; set;}
    public string OTHER_CARDIAC_LVA {get; set;}
    public string OTHER_CARDIAC_VSD {get; set;}
    public string OTHER_CARDIAC_ASD {get; set;}
    public string OTHER_CARDIAC_BATISTA {get; set;}
    public string OTHER_CARDIAC_CONGENITAL {get; set;}
    public string OTHER_CARDIAC_LASER {get; set;}
    public string OTHER_CARDIAC_TRAUMA {get; set;}
    public string OTHER_CARDIAC_TX {get; set;}
    public string OTHER_CARDIAC_PACEMAKER {get; set;}
    public string OTHER_CARDIAC_AICD {get; set;}
    public string OTHER_CARDIAC_OTHER {get; set;}
    public string OTHER_NONCARD {get; set;}
    public string OTHER_NONCARDIAC_AORTIC_ANEURYSM {get; set;}
    public string OTHER_NONCARDIAC_ASC {get; set;}
    public string OTHER_NONCARDIAC_ARCH {get; set;}
    public string OTHER_NONCARDIAC_DESC {get; set;}
    public string OTHER_NONCARDIAC_THOR {get; set;}
    public string OTHER_NONCARDIAC_ABD {get; set;}
    public string OTHER_NONCARDIAC_CAROTID_ENDARTERECTOMY {get; set;}
    public string OTHER_NONCARDIAC_VASCULAR {get; set;}
    public string OTHER_NONCARDIAC_THORACIC {get; set;}
    public int CONVERSION_TO_CPB {get; set;}
    public string SURG_VENTRICULAR_RESTORATION {get; set;}
    public int PREDICTED_DEEP_STERNAL_WOND_INFX {get; set;}
    public int PREDICTED_REOPERATION {get; set;}
    public int PREDICTED_PERMANENT_STROKE {get; set;}
    public int PREDICTED_PROLONGED_VENTILATION {get; set;}
    public int PREDICTED_RENAL_FAILURE {get; set;}
    public int PREDICTED_MORBIDITY_OR_MORTALITY {get; set;}
    public int PREDICTED_SHORT_LEN_STAY {get; set;}
    public int PREDICTED_LONG_LEN_STAY {get; set;}
    
}

