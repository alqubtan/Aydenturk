namespace Aydinturk_agency.Utils
{
    public class GetTime
    {
        public static string GetMessage()
        {
            var GreetingMSG = "";
            if (DateTime.UtcNow.AddHours(3).Hour < 12)
            {
                GreetingMSG = "صباح الخير";
            }
            else if (DateTime.UtcNow.AddHours(3).Hour < 17)
            {
                GreetingMSG = "طاب مسائك";
            }
            else
            {
                GreetingMSG = "مساء الخير";
            }
            return GreetingMSG;
        }
    }
}
