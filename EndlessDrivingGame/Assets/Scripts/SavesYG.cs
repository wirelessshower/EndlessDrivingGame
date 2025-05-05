namespace YG {
    public partial class SavesYG {
        public int money = 10000; // Начальное количество денег
        public bool[] carsBought; // Массив для хранения статуса покупки машин
        public int currentCarIndex = 0; // Индекс текущей выбранной машины
        public int currentLevelMoney = 0; // Количество монет на текущем уровне

        // Конструктор для инициализации массива при первом запуске (если необходимо)
        public SavesYG() {
            if (carsBought == null)
                carsBought = new bool[/* Количество машин в магазине */ 5]; // Замени на реальное количество
        }

        // Метод для сброса монет текущего уровня
        public void ResetLevelMoney() {
            currentLevelMoney = 0;
        }
    }
}