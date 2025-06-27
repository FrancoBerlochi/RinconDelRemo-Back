using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models;

namespace Application.Interfaces
{
    public interface IHangerService
    {
        HangerDto Create(HangerCreateRequest request, int kayakId);
        void Delete(int id);
        List<HangerDto> GetOccupiedHangers();
        HangerDto? GetById(int id);
        IEnumerable<HangerDto> GetHangersByOwner(string ownerId);
        List<string> GetFreeHangers();
        List<HangerStatusDto> GetAllHangerStatus();

    }
}
