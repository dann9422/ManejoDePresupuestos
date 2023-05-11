namespace ManejoDePresupuestos.Servicios
{

    public interface IservicioUsuarios
    {
        int ObtenerUsuarioId();
    }


    public class ServicioUsuarios : IservicioUsuarios
    {

        public int ObtenerUsuarioId()
        {
            return 1;   
        }
    }
}
