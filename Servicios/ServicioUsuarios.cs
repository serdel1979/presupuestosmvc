namespace WebApplication1.Servicios
{

    public interface IServicioUsuarios
    {
        int GetUsuarioId();
    }
    public class ServicioUsuarios : IServicioUsuarios
    {
        public int GetUsuarioId()
        {
            return 1;
        }
    }
}
