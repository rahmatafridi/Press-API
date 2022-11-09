namespace PressDot.Core.Enums
{

    #region Saloon Exception Codes: Range  >= 21 and <=40

    public enum SaloonExceptionsCodes
    {
        Saloon_Not_Found = 21,
        Pending_Appointments_Found_For_Saloon = 22,
        Something_Went_Wrong_While_Deleting_Saloon = 23

    }

    #endregion

    #region User Exception Codes: Range  >= 41 and <=60

    public enum UserExceptionsCodes
    {
        User_Not_Found = 41,
        No_Roles_Found_Against_Provided_User = 42,
        No_Roles_Found_Against_Provided_RoleIds = 43,
        Something_Went_Wrong_While_Deleting_User = 44,
        Pending_Appointments_Found_For_User = 45,
        Invalid_Role_Id = 46


    }

    #endregion

    #region Saloon Laboratory Exception Codes: Range  >= 61 and <=80

    public enum SaloonLaboratoryExceptionsCodes
    {
        Laboratory_Not_Found = 61,
        Saloon_Not_Found = 62,
        Laboratory_Already_Attached = 63,
        Something_Went_Wrong_While_Updating_SaloonLaboratory = 64,
        Some_Thing_went_wrong_while_deleting_SaloonLaboratory = 65,
        Unable_To_Delete_Laboratory_Due_To_Penidng_Orders =66,


    }

    #endregion

    #region General Exception Codes: Range  >= 100 and <=120

    public enum GeneralExceptionsCodes
    {
        Something_Went_Wrong = 100,
        Invalid_Id = 101

    }

    #endregion

    #region SaloonEmployee Exception Codes: Range  >= 121 and <=140

    public enum SaloonEmployeeExceptionsCodes
    {
        Employee_Already_Scheduled = 121,
        Some_Thing_went_wrong_while_deleting_Schedule = 122,
        Some_Thing_Went_Wrong_While_deleting_Employee_Association_With_Saloon=123


    }

    #endregion

    #region Appointment Exception Codes: Range >=141 and <=160
    public enum AppointmentExceptionsCodes
    {
        NoSaloonAssociatedWithUser = 141


    }
    #endregion


}