

namespace Notification_Service.Template
{
    public class EmailTemplate
    {
        public static string ActiveAccount(string UserName, string link) 
        {
            return $@"
                Dear {UserName},

                Thank you for signing up for WhileLagoon! We're excited to have you as a new member of our community.

                To complete your registration, please click on the verification link below to activate your account: {link}

                Once your email is verified, you'll be able to log in to your account and start exploring all of the amazing features that Fasty has to offer.

                If you have any questions or need assistance with anything, please don't hesitate to reach out to our support team at datttp113@gmail.com.

                Thank you again for choosing WhileLagoon. We look forward to seeing you inside!

                Best regards,
                WhileLagoon Team
            ";
        }

        public static string ForgotPassword(string UserName, string link)
        {
            return $@"
                Dear {UserName},

                As part of our ongoing commitment to keeping your account secure, we recommend changing your password from time to time. If you'd like to change your password now, please click on this link: {link}

                This link will take you to a secure page where you can enter your new password. After submitting your new password, you'll be able to log in to your account using your updated credentials.

                If you did not request a password change, or if you have any concerns about the security of your account, please contact our support team at datttp113@gmail.com.

                Thank you for choosing WhileLagoon. We appreciate your business and are committed to providing you with the best possible user experience.

                Best regards,
                WhileLagoon Team
            ";
        }
    }
}