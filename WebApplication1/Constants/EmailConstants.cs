namespace ReviewPlatformAPI.Constants
{
 
   public class EmailConstants
    {
        public static string ReplyToEmail = "cwdbreeding@outlook.com";

        //Subjects
        public static string ChangePasswordSubject = "CWD Breeding Account - Set-up/Change Password";

        public static string DenyDeerSubject = "Deer Listing Request Denied";

        public static string DeerSubmissionSubject = "Submission of Deer Listing";

        public static string ApproveDeerRequestSubject = "Confirmation of Deer Listing";




        //Bodies
        public static string ChangePasswordBody = "{0},<br><br>A request has been sent to set-up/change your password for your " +
                                                    "CWD Breeding Ranch Account,   " +
                                                    "if this was you click <a href='{1}'>here</a> " +
                                                    "to reset your password.<br><br>If this was not you, please ignore this email.";

        public static string DenyDeerBody = "{0},<br><br>Thank you for using CWDBreeding.com. Your request for listing <strong>{1}</strong> has been denied for the following reason.<br><br>" +
                                            "Reason: {2} <br><br> Please update the record and submit for review.<br><br>Click <a href='{3}'>here</a> to login<br><br>Best Regards!";

        public static string DeerSubmissionBody = "Thank you {0} for your submission of <strong>{1}</strong> on <a href='{2}'>CWDBreeding.com</a><br><br>" +
                                                "Your submission is being reviewed by our staff and an invoice for the listing will be sent to you shortly.<br><br>" +
                                                "Onward and Upward!<br><br>Sincerely,<br><br>The Deer Wizard, Josh Newton"; 
        
        public static string ApproveDeerRequestBody = "Congratulations <strong>{0}</strong> has been approved for <a href='{1}'>CWDBreeding.com</a><br><br>" +
                                                        "Onward and Upward!<br><br>Sincerely,<br><br>The Deer Wizard, Josh Newton";                                     


    }
}
