namespace GM.CommonLibs.Helper
{
    public static class Globals
    {
        public static int GetStatusOrder(string status)
        {
            if (status != null)
            {
                switch (status.ToLower())
                {
                    case "delete": return 1;
                    case "create": return 2;
                    case "update": return 3;
                    default: return 99;
                }
            }
            else
            {
                return 99;
            }
        }
    }
}
