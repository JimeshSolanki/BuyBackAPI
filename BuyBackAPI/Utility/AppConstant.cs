using System;

namespace BuyBackAPI.Utility
{
    public class AppConstant
    {
        public const int STATUS_SUCCESS = 1;
        public const int STATUS_FAILED = 0;

        public const string INSERT_MESSAGE = "Record Inserted Successfully.";
        public const string UPDATE_MESSAGE = "Record Updated Successfully.";
        public const string DELETE_MESSAGE = "Record Deleted Successfully.";

        public const string INVALID_ID = "Invalid ID Found.";

        public const string RECORD_FOUNT_MESSAGE = "Record Found.";
        public const string RECORD_NOT_FOUND_MESSAGE = "Record Not Found.";

        public static string ToStr(string str)
        {
            return isStr(str) ? str : string.Empty;
        }

        public static bool isStr(string str)
        {
            return !String.IsNullOrEmpty(str) ? true : false;
        }
    }
}
