namespace ReviewPlatformAPI.Constants
{
 
   public class EmailConstants
    {
        public static string ReplyToEmail = "cwdbreeding@outlook.com";

        //Subjects
        public static string ChangePasswordSubject = "CWD Breeding Account - Set-up/Change Password";

        public static string DenyDeerSubject = "Deer Listing Request Denied";


        //Bodies
        public static string ChangePasswordBody = "{0},<br><br>A request has been sent to set-up/change your password for your " +
                                                    "CWD Breeding Ranch Account,   " +
                                                    "if this was you click <a href='{1}'>here</a> " +
                                                    "to reset your password.<br><br>If this was not you, please ignore this email.";

        public static string DenyDeerBody = "{0},<br><br>Your request for the deer {1} to be listed has been denied for the following reason <br><br>" +
                                            "{2} <br><br> Please fix issues and re-submit deer.";
                                                   
    }
}
