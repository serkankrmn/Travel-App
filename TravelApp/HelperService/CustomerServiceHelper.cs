using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Data.DomainClasses;

namespace TravelApp.HelperService
{
    public class CustomerServiceHelper
    {
            private readonly ApplicationDbContext _context;
            public CustomerServiceHelper(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CustomerService> FindMostSuitableCustomerService()
            {

                if (!_context.CustomerServiceTasks.Any())
                {

                    return await _context.CustomerServices.FirstAsync();

                }


            var taskGroupData = await (from t in _context.CustomerServiceTasks
                                           group t by t.CustomerServiceId into g
                                           select new
                                           {
                                               id = g.Key,
                                               countOfWork = g.Count()
                                           }).ToListAsync();



                var findedCustomerService = taskGroupData.FirstOrDefault(t => t.countOfWork == taskGroupData.Min(a => a.countOfWork));




                if (findedCustomerService == null) { throw new Exception("Hiç müşteri temsilcisi yoktur..."); }


                var findedCustemerServiceId = findedCustomerService.id;

                return await _context.CustomerServices.FindAsync(findedCustemerServiceId)!;
            }
    }
}
