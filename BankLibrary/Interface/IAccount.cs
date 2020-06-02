
namespace BankLibrary.Interface
{
    interface IAccount
    {
        /// <summary>
    /// Положит на деньги на счет
    /// </summary>
    /// <param name="sum"></param>
        void Pat(decimal sum);
        /// <summary>
        /// Взьят со счета
        /// </summary>
        /// <param name="sum"></param>
        decimal Withdraw(decimal sum);
    }
}
