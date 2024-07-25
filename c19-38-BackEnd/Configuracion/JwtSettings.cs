namespace c19_38_BackEnd.Configuracion
{
    internal class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }

        public bool ValidateIssuer { get; set; }
        public string? ValidIssuer { get; set; }

        public bool ValidateAudience { get; set; }
        public string? ValidAudience { get; set; }

        public bool RequiredExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; }
    }
}
