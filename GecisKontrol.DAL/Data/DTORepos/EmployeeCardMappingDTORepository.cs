using Dapper;
using GecisKontrol.Domain.DTOs;
using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.DAL.Data.DTORepos
{
    public class EmployeeCardMappingDTORepository : IEmployeeCardMappingDTORepository
    {
        private readonly DbContext _dbContext;

        public EmployeeCardMappingDTORepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<IEnumerable<EmployeeCardMappingDTO>> GetEmployeeCardMappingsAsync(string query, object param)
        {
        //    var query = @"
        //SELECT 
        //    ecm.id, 
        //    ecm.cardid, 
        //    c.cardhex, 
        //    ecm.employeeid, 
        //    e.name AS EmployeeName, 
        //    ecm.isactive, 
        //    ecm.creationdate
        //FROM EmployeeCardMapping ecm
        //JOIN Card c ON ecm.cardid = c.id
        //JOIN Employee e ON ecm.employeeid = e.id";


            using var connection = _dbContext.CreateConnection();
            {
                return await connection.QueryAsync<EmployeeCardMappingDTO>(query,param);
            }
        }
    }
}
