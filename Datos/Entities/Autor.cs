namespace Datos.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int Edad { get; set; }

        public string Nacionalidad { get; set; }

        public List<AutorLibro> AutorLibros { get; set; }
    }
}
