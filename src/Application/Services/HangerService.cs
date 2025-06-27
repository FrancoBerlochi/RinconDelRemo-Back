using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class HangerService : IHangerService
    {
        private readonly IHangerRepository _hangerRepository;
        private readonly IKayakRepository _kayakRepository;
        public HangerService(IHangerRepository hangerRepository, IKayakRepository kayakRepository)
        {
            _hangerRepository = hangerRepository;
            _kayakRepository = kayakRepository;
        }

        public HangerDto Create(HangerCreateRequest request, int kayakId) // validar que el dueno lo cree
        {
            var kayak = _kayakRepository.GetById(kayakId) ?? throw new NotFoundException("Kayak no encontrado.");

            if (_hangerRepository.IsKayakAssigned(kayakId))
            {
                throw new InvalidOperationException("Ese kayak ya está asignado a una percha.");
            }

            if (request.Row < 1 || request.Row > 10)
            {
                throw new ArgumentException("La fila debe estar entre 1 y 10.");
            }

            char upperColumn = char.ToUpper(request.Column);
            if (upperColumn < 'A' || upperColumn > 'J')
            {
                throw new ArgumentException("La columna debe estar entre A y J.");
            }

            var existing = _hangerRepository.FindByRowAndColumn(request.Row, upperColumn);
            if (existing != null)
            {
                throw new InvalidOperationException("Ya existe una percha en esa ubicación.");
            }

            var hanger = new Hanger
            {
                Row = request.Row,
                Column = upperColumn,
                IsOccupied = true,
                KayakId = kayakId,
                Kayak = kayak
            };

            _hangerRepository.Create(hanger);
            return HangerDto.Create(hanger);
        }

        public void Delete(int id) //validar que solo el encargado lo pueda hacer
        {
            var hanger = _hangerRepository.GetById(id);
            if (hanger == null)
            {
                throw new NotFoundException($"Percha no encontrada.");
            }

            _hangerRepository.Delete(hanger);
        }

        public List<HangerDto> GetOccupiedHangers() // validar que solo lo haga el encargado, muestra perchas ocupadas
        {
            var hangers = _hangerRepository.GetAll();
            return hangers.Select(HangerDto.Create).ToList();
        }

        public HangerDto? GetById(int id) // solo encargado
        {
            var hanger = _hangerRepository.GetById(id) ?? throw new NotFoundException("Percha no encontrada.");
            return HangerDto.Create(hanger);
        }

        public IEnumerable<HangerDto> GetHangersByOwner(string ownerId) //muestra perchas de cada dueno
        {
            var hangers = _hangerRepository.GetByOwnerId(ownerId) ?? throw new NotFoundException("Dueño no encontrado.");
            return hangers.Select(HangerDto.Create);
        }

        public List<string> GetFreeHangers() // muestra perchas disponibles
        {
            var ocupadas = _hangerRepository
                .GetOccupied()
                .Select(h => $"{h.Row}{char.ToUpper(h.Column)}")
                .ToHashSet();

            var freeHangers = new List<string>();

            for (int fila = 1; fila <= 10; fila++)
            {
                for (char col = 'A'; col <= 'J'; col++)
                {
                    var lugar = $"{fila}{col}";
                    if (!ocupadas.Contains(lugar))
                    {
                        freeHangers.Add(lugar);
                    }
                }
            }

            return freeHangers;
        }

        public List<HangerStatusDto> GetAllHangerStatus() //muestra el estado de todas las perchas
        {
            var allHangers = _hangerRepository.GetAll(); // todas las perchas existentes en DB
            var occupied = allHangers
                .Where(h => h.IsOccupied)
                .Select(h => new HangerStatusDto
                {
                    Location = $"{h.Row}{char.ToUpper(h.Column)}",
                    IsOccupied = true,
                    KayakId = h.KayakId
                })
                .ToList();

            var occupiedLocations = new HashSet<string>(occupied.Select(h => h.Location));
            var fullGrid = new List<HangerStatusDto>();

            // Generar toda la grilla (10x10)
            for (int row = 1; row <= 10; row++)
            {
                for (char col = 'A'; col <= 'J'; col++)
                {
                    var loc = $"{row}{col}";
                    if (!occupiedLocations.Contains(loc))
                    {
                        fullGrid.Add(new HangerStatusDto
                        {
                            Location = loc,
                            IsOccupied = false,
                            KayakId = null
                        });
                    }
                }
            }

            fullGrid.AddRange(occupied); // agregar los ocupados
            return fullGrid.OrderBy(h => int.Parse(h.Location.Substring(0, h.Location.Length - 1)))
                   .ThenBy(h => h.Location[^1]).ToList();
        }
    }
}
