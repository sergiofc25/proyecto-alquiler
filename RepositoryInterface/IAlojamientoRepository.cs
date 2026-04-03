using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IAlojamientoRepository
{
    IEnumerable<Ent_Alojamiento> Obten();
    public string Actualiza(Ent_Alojamiento Alojamiento);
    public Ent_Alojamiento Obten_x_Id(int Alo_Id);


}