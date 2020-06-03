
namespace BankLibrary.Model
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Сумма, на которую изменился счет
        /// </summary>
        public decimal Sum { get; private set; }
        public AccountEventArgs(string _mes,decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
