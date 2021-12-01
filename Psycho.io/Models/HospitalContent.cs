using System.Collections.Generic;

namespace Psycho.io.Models
{
    public class HospitalContent
    {
        public string Lang { get; set; }
        
        public HospitalContent_Layout Layout { get; set; }
        public HospitalContent_Home Home { get; set; }
        public HospitalContent_Account Account { get; set; }
        public HospitalContent_Admin Admin { get; set; }
        public HospitalContent_Diary Diary { get; set; }
        public HospitalContent_HeartDiseasePrediction HeartDiseasePrediction { get; set; }
        public HospitalContent_Doctors Doctors { get; set; }
        public HospitalContent_Users Users { get; set; }
    }

    public class HospitalContent_Layout
    {
        public string Home { get; set; }
        public string DoctorsNav { get; set; }
        public string ToolsNav { get; set; }
        public string HeartDiseasePredictionNav { get; set; }
        public string AboutNav { get; set; }
        public string SignIn { get; set; }
        public string SignUp { get; set; }
    }
    public class HospitalContent_Account
    {
        public HospitalContent_Account_SignIn SignIn { get; set; }
        public HospitalContent_Account_SignUp SignUp { get; set; }
        
        public class HospitalContent_Account_SignIn
        {
            public string Title { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string RememberMe { get; set; }
            public string SignInBtn { get; set; }
            public string Alternative { get; set; }
            public string NotAMember { get; set; }
            public string SignUp { get; set; }
        }
        
        public class HospitalContent_Account_SignUp
        {
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneNumberFormat { get; set; }
            public string Gender { get; set; }
            public string Male { get; set; }
            public string Female { get; set; }
            public string Other { get; set; }
            public string Age { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }
            public string SignUp { get; set; }
        }
    }
    public class HospitalContent_Home
    {
        public HospitalContent_Home_Index Index { get; set; }
        
        public class HospitalContent_Home_Index
        {
            public string EnjoyLife { get; set; }
            public string Description { get; set; }
            public string GetAdvice { get; set; }
            public string LearnMore { get; set; }
        }
    }
    public class HospitalContent_Admin
    {
        public HospitalContent_Admin_Index Index { get; set; }
        public HospitalContent_Admin_ReportsForReview ReportsForReview { get; set; }
        public HospitalContent_Admin_CreateDoctor CreateDoctor { get; set; }
        public HospitalContent_Admin_DeleteDoctor DeleteDoctor { get; set; }
        public HospitalContent_Admin_EditDoctor EditDoctor { get; set; }

        public class HospitalContent_Admin_Index
        {
            public string PageTitle { get; set; }
            public List<string> ColumnHeaders { get; set; }
            public string ReportsForReviewBtn { get; set; }
            public string AddDoctorBtn { get; set; }
            public string EditDoctorBtn { get; set; }
            public string DeleteDoctorBtn { get; set; }
        }
        public class HospitalContent_Admin_ReportsForReview
        {
            public string PageTitle { get; set; }
            public string ApproveReportBtn { get; set; }
            public string RemoveReportBtn { get; set; }
        }
        public class HospitalContent_Admin_CreateDoctor
        {
            public string PageTitle { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneNumberFormat { get; set; }
            public string Gender { get; set; }
            public string Male { get; set; }
            public string Female { get; set; }
            public string Other { get; set; }
            public string HireDate { get; set; }
            public string CreateBtn { get; set; }
        }
        public class HospitalContent_Admin_DeleteDoctor
        {
            public string PageTitle { get; set; }
            public string Confirmation { get; set; }
            public string CancelBtn { get; set; }
            public string DeleteBtn { get; set; }
        }
        public class HospitalContent_Admin_EditDoctor
        {
            public string PageTitle { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneNumberFormat { get; set; }
            public string Gender { get; set; }
            public string Male { get; set; }
            public string Female { get; set; }
            public string Other { get; set; }
            public string HireDate { get; set; }
            public string UpdateBtn { get; set; }
        }
    }
    public class HospitalContent_Diary
    {
        public HospitalContent_Diary_Nav Nav { get; set; }
        public HospitalContent_Diary_HealthDiary HealthDiary { get; set; }
        public HospitalContent_Diary_Statistics Statistics { get; set; } 
        
        public class HospitalContent_Diary_Nav
        {
            public string HealthDiary { get; set; }
            public string Statistics { get; set; }
        }
        
        public class HospitalContent_Diary_HealthDiary
        {
            public string Lifestyle { get; set; }
            public string Symptoms { get; set; }
            public string Mood { get; set; }
        }
        
        public class HospitalContent_Diary_Statistics
        {
            public string Water { get; set; }
            public string Sleep { get; set; }
            public string Weight { get; set; }
            public string Temperature { get; set; }
        }
    }
    
    public class HospitalContent_HeartDiseasePrediction
    {
        public HospitalContent_HeartDiseasePrediction_Index Index { get; set; }
        
        public class HospitalContent_HeartDiseasePrediction_Index
        {
            public string PageTitle { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string Male { get; set; }
            public string Female { get; set; }
            public string ChestPainType { get; set; }
            public string TypicalAngina { get; set; }
            public string AtypicalAngina { get; set; }
            public string NonAnginalPain { get; set; }
            public string Asymptomatic { get; set; }
            public string RestingBloodPressure { get; set; }
            public string SerumCholestrol { get; set; }
            public string FastingBloodSugar { get; set; }
            public string RestingEcg { get; set; }
            public string Normal { get; set; }
            public string HavingSTTWaveAbnormality { get; set; }
            public string ShowingProbableOrDefiniteLeftVentricularHypertrophyByEstesCriteria { get; set; }
            public string MaxHeartRateAchieved { get; set; }
            public string ExerciseInducedAngina { get; set; }
            public string Yes { get; set; }
            public string No { get; set; }
            public string StDepressionInducedByExercise { get; set; }
            public string PeakExerciseStSegment { get; set; }
            public string Upsloping { get; set; }
            public string Flat { get; set; }
            public string Downsloping { get; set; }
            public string NumberOfMajorVessels { get; set; }
            public string Thalassemia { get; set; }
            public string NormalTh { get; set; }
            public string FixedDefect { get; set; }
            public string ReversibleDefect { get; set; }
            public string Predict { get; set; }
            public string HeartDiseasePredictionResult { get; set; }
            public string SuccessPredictionResult { get; set; }
            public string Close { get; set; }
            public string WarningPredictionResult1 { get; set; }
            public string WarningPredictionResult2 { get; set; }
            public string ViewDoctorsBtn { get; set; }
            public string CreateAccountBtn { get; set; }
            public string ParametersDescription { get; set; }
            public string ChestPainTitle { get; set; }
            public string ChestPainDescription { get; set; }
            public string RestingBloodPressureTitle { get; set; }
            public string RestingBloodPressureDescription { get; set; }
            public string SerumCholesterolTitle { get; set; }
            public string SerumCholesterolDescription { get; set; }
            public string FastingBloodSugarTitle { get; set; }
            public string FastingBloodSugarDescription { get; set; }
            public string MaxHeartRateAchievedTitle { get; set; }
            public string MaxHeartRateAchievedDescription { get; set; }
            public string ExerciseInducedAnginaTitle { get; set; }
            public string ExerciseInducedAnginaDescription { get; set; }
        }
    }

    public class HospitalContent_Doctors
    {
        public HospitalContent_Doctors_Index Index { get; set; }
        public HospitalContent_Doctors_AddReport AddReport { get; set; }
        public HospitalContent_Doctors_CreateAppointment CreateAppointment { get; set; }
        public HospitalContent_Doctors_Reports Reports { get; set; }
        public HospitalContent_Doctors_Schedule Schedule { get; set; }
        
        public class HospitalContent_Doctors_Index
        {
            public string PageTitle { get; set; }
            public List<string> GeneralColumnHeaders { get; set; }
            public string ScheduleBtn { get; set; }
            public string SignBtn { get; set; }
            public string ChatBtn { get; set; }
            public string ViewAllReportsBtn { get; set; }
            public string AddReportBtn { get; set; }
            public string PreviousBtn { get; set; }
            public string NextBtn { get; set; }
        }

        public class HospitalContent_Doctors_AddReport
        {
            public string AddReportFor { get; set; }
            public string YourReport { get; set; }
            public string Anonymous { get; set; }
            public string AddBtn { get; set; }
        }

        public class HospitalContent_Doctors_CreateAppointment
        {
            public string PageTitle { get; set; }
            public string AdditionalInfo { get; set; }
            public string CreateBtn { get; set; }
        }

        public class HospitalContent_Doctors_Reports
        {
            public string PageTitle { get; set; }
            public string NoReportsMessage { get; set; }
        }

        public class HospitalContent_Doctors_Schedule
        {
            public string PageTitle { get; set; }
        }
    }

    public class HospitalContent_Users
    {
        public HospitalContent_Users_AddAnalysis AddAnalysis { get; set; }
        public HospitalContent_Users_Chat Chat { get; set; }
        public HospitalContent_Users_CreateAppointment CreateAppointment { get; set; }
        public HospitalContent_Users_DashboardAuthorizedUser DashboardAuthorizedUser { get; set; }
        public HospitalContent_Users_DashboardDoctor DashboardDoctor { get; set; }
        public HospitalContent_Users_FinishAppointment FinishAppointment { get; set; }
        public HospitalContent_Users_Profile Profile { get; set; }
        public HospitalContent_Users_ShowAppointmentResult ShowAppointmentResult { get; set; }
        
        public class HospitalContent_Users_AddAnalysis
        {
            public string PageTitle { get; set; }
            public string Name { get; set; }
            public string AnalysisResult { get; set; }
            public string DoctorConclusion { get; set; }
            public string ChooseFile { get; set; }
            public string SaveChanges { get; set; }
        }
        public class HospitalContent_Users_Chat
        {
            public string Members { get; set; }
            public string NoMessages { get; set; }
            public string SelectChat { get; set; }
            public string TypeMessageHere { get; set; }
            public string SendBtn { get; set; }
        }
        public class HospitalContent_Users_CreateAppointment
        {
            public string PageTitle { get; set; }
            public string Doctors { get; set; }
            public string Patient { get; set; }
            public string AdditionalInfo { get; set; }
            public string CreateBtn { get; set; }
        }
        
        public class HospitalContent_Users_DashboardAuthorizedUser
        {
            public string TitlePart1 { get; set; }
            public string TitlePart2 { get; set; }
            public string NoActiveAppointments { get; set; }
            public string Patient { get; set; }
            public string Profile { get; set; }
        }
        
        public class HospitalContent_Users_DashboardDoctor
        {
            public string TitlePart1 { get; set; }
            public string TitlePart2 { get; set; }
            public string NoActiveAppointments { get; set; }
            public string Doctor { get; set; }
            public string Profile { get; set; }
        }

        public class HospitalContent_Users_FinishAppointment
        {
            public string PageTitle { get; set; }
            public string Services { get; set; }
            public string AddServices { get; set; }
            public string SaveChangesBtn { get; set; }
        }

        public class HospitalContent_Users_Profile
        {
            public string EditProfileTitle { get; set; }
            public string CompleteYourProfile { get; set; }
            public string Email { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneNumberFormat { get; set; }
            public string Gender { get; set; }
            public string Male { get; set; }
            public string Female { get; set; }
            public string Other { get; set; }
            public string HireDate { get; set; }
            public string Position { get; set; }
            public string Age { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }
            public string WorkSchedule { get; set; }
            public string MondayStart { get; set; }
            public string MondayEnd { get; set; }
            public string TuesdayStart { get; set; }
            public string TuesdayEnd { get; set; }
            public string WednesdayStart { get; set; }
            public string WednesdayEnd { get; set; }
            public string ThursdayStart { get; set; }
            public string ThursdayEnd { get; set; }
            public string FridayStart { get; set; }
            public string FridayEnd { get; set; }
            public string SaveBtn { get; set; }
            public string Doctor { get; set; }
            public string Patient { get; set; }
            public string Schedule { get; set; }
        }

        public class HospitalContent_Users_ShowAppointmentResult
        {
            public string PageTitle { get; set; }
            public string Patient { get; set; }
            public string Age { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            public string Doctor { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
            public string FinishedStatus { get; set; }
            public string PendingStatus { get; set; }
            public string PlannedStatus { get; set; }
            public string СontinuesStatus { get; set; }
            public string FinishBtn { get; set; }
            public string Analyzes { get; set; }
            public string AddAnalyzesBtn { get; set; }
            public string Name { get; set; }
            public string Result { get; set; }
            public string DoctorConclusion { get; set; }
            public string File { get; set; }
            public string Remove { get; set; }
            public string DownloadBtn { get; set; }
            public string ResultNotAvailable { get; set; }
        }
    }
}