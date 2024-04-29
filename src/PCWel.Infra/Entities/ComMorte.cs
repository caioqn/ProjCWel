namespace PCWel.Infra.Entities
{
    public class ComMorte
    {
        public int com_morte_ani_pk { get; set; }
        public int com_morte_ani_fk { get; set; }
        public int com_morte_criador_fk { get; set; }
        public string? com_morte_ani_nome { get; set; }
        public int com_morte_tipo_fk { get; set; }
        public string? com_morte_causa_morte { get; set; }
        public int com_morte_protocolo { get; set; }
        public DateTime com_morte_ani_dt_morte { get; set; }
        public DateTime com_morte_ani_dt_com { get; set; }
        public DateTime com_morte_ani_ts { get; set; }
       

        public ComMorte()
        {
        }
    }
}
