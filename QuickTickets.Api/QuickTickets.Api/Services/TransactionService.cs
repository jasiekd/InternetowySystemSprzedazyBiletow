using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTickets.Api.Data;
using QuickTickets.Api.Dto;
using QuickTickets.Api.Entities;

namespace QuickTickets.Api.Services
{

        public class TransactionService
        {
            private readonly AccountService _accountService;
            private readonly DataContext _context;
            public TransactionService(AccountService accountService, DataContext context)
            {
                _accountService = accountService;
                _context = context;
            }



            public async Task<IActionResult> GetTransactionsForAdmin(PaginationDto paginationDto)
            {
                try
                {
                    var data = _context.Transactions.AsQueryable().Include(x => x.User).Where(x => x.Status == StatusEnum.Pending.ToString());


                return await GetPaginatedTransactions(paginationDto, data);

                }
                catch (Exception ex)
                {
                    throw;
                }
            }


            private async Task<IActionResult> GetPaginatedTransactions(PaginationDto paginationDto, IQueryable<TransactionEntity> data)
            {
                var totalCount = await data.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationDto.pageSize);

                data = data.Skip((paginationDto.pageIndex - 1) * paginationDto.pageSize).Take(paginationDto.pageSize);

                var transactionList = new List<dynamic>();

                foreach (var temp in await data.ToListAsync())
                {
                transactionList.Add(new
                    {
                        TransactionID = temp.TransactionID,
                        User = _accountService.GetUserInfoDto(temp.User),
                        Price = temp.Price,
                        TransactionDate = temp.DateCreated,
                        
                    });
                }

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageIndex = paginationDto.pageIndex,
                    PageSize = paginationDto.pageSize,
                    Transactions = transactionList
                };
                return new OkObjectResult(result);
            }
        }
    }

