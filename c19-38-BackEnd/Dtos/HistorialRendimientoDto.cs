namespace c19_38_BackEnd.Dtos
{
    public class HistorialRendimientoDto
    {
        public int IdHistorial { get; set; }
        public int cantFlexionesMin { get; set; }
        public int cantSentadillasMin { get; set; }
        public DateTime CienMtsTiempo { get; set; }
        public DateTime FechaGuardado { get; set; }
        public string? Nota { get; set; }
        public int IdUsuario { get; set; }
    }
}
