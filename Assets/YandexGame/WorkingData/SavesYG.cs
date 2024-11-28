
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Свои данные: 
        public int Balance = 3000;
        public int BetValue = 10;
        public bool IsFirstStart = true;
        public SavesYG()
        {
        }
    }
}
