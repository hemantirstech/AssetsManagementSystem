using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;

namespace AssetsWebApi.Repository.Contract
{
    public interface ITransactionProductHistoryInterface<TransactionProductHistoryResult>
    {
        IEnumerable<TransactionProductHistoryResult> GetAllASTransactionProductHistory();

        TransactionProductHistoryResult GetASTransactionProductHistoryByID(long TransactionProductHistoryId);

        Task InsertASTransactionProductHistory(ASTransactionProductHistory objASTransactionProductHistory);
        Task UpdateASTransactionProductHistory(ASTransactionProductHistory objASTransactionProductHistory);
        Task DeleteASTransactionProductHistory(long TransactionProductHistoryId);
        bool ASTransactionProductHistoryExists(long TransactionProductHistoryId);
    }
}
