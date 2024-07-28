namespace c19_38_BackEnd.Dtos
{
    public class SerieDto
    {
        public int IdSerie { get; set; }
        public string Descripcion { get; set; }
        public int CantidadSeries { get; set; }
        public int? CantidadRepeticiones { get; set; }
        public DateTime TiempoDescanso { get; set; }
        public DateTime? Duracion { get; set; }
        public int IdPlan { get; set; }
        public int IdEjercicio { get; set; }
    }
}
