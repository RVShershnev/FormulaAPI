namespace FormulaApi
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly FormulaDbContext context;
        public FormulaRepository(FormulaDbContext context)
        {
            this.context = context;

            if (context.Items.Any())
            {
                return;
            }
        }
        
    }
}
