namespace PCWel.Infra.Entities
{
    public class Animal 
    {
        public int ani_pk { get; set; }
        public string? ani_nome { get; set; }
        public string? ani_nome_usual { get; set; }
        public DateTime? ani_dt_nasc { get; set; }
        public string? ani_ativo { get; set; }

        public Animal()
        {
            
        }
    }
}
