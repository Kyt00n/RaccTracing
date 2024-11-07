using System.Text;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Application.Interfaces;

public interface ICameraService
{
    void Render(StringBuilder output, Hittable world);
    
}