using BankLibrary.Interface;
namespace BankLibrary.Model
{
    public abstract class Account : IAccount
    {
        /// <summary>
        /// Событие, возникающее при выводе денег
        /// </summary>
        protected internal event AccountStateHandler Withreved;

        /// <summary>
        /// Событие возникающее при добавление на счет
        /// </summary>
        protected internal event AccountStateHandler Added;

        /// <summary>
        /// Событие возникающее при открытии счета
        /// </summary>
        protected internal event AccountStateHandler Opened;

        /// <summary>
        /// Событие возникающее при закрытии счета
        /// </summary>
        protected internal event AccountStateHandler Closed;

        /// <summary>
        /// Событие возникающее при начислении процентов
        /// </summary>
        protected internal event AccountStateHandler Calculated;

        static int counter = 0;

        /// <summary>
        /// время с момента открытия счета
        /// </summary>
        protected int _days = 0;

        /// <summary>
        /// Текущая сумма на счету
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Процент начислений
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// Уникальный идентификатор счета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// вызов событий
        /// </summary>
        /// <param name="e"></param>
        /// <param name="handler"></param>
        private void CallEvent(AccountEventArgs e,AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this,e);
            }
        }

    
        #region вызов отдельных событий. Для каждого события определяется свой витуальный метод
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        private virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withreved);
        }
        private virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e,Calculated);
        }
        #endregion
        public virtual void Pat(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило"+sum,sum));
        }

        /// <summary>
        /// метод снятия со счета, возвращает сколько снято со счета
        /// </summary>
        /// <param name="sum"></param>
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета {Id}",sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }

        /// <summary>
        /// открытие счета
        /// </summary>
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет! Id счета: {Id}",Sum));
        }

        /// <summary>
        /// закрытие счета
        /// </summary>
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счет {Id} закрыт.  Итоговая сумма: {Sum}", Sum));
        }
        
        protected internal void IncrementDays()
        {
            _days++;
        }

        /// <summary>
        /// начисление процентов
        /// </summary>
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере: {increment}", increment)));
        }
    }
}
