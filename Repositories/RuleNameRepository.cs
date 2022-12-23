using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi3.Domain;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly LocalDbContext _context;
        public RuleNameRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that GETs the list of RuleName from the database,
        /// then converts it into a list of RuleNameDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <returns></returns>
        public async Task<List<RuleNameDTO>> GetAllRuleNames()
        {
            var ruleNames = await _context.RuleName
                .Select(x => RuleNameToDTO(x))
                .ToListAsync();

            return ruleNames;
        }

        /// <summary>
        /// Method that GETs a specific RuleName by its Id from the database,
        /// then converts it into a RuleNameDTO (showing less properties)
        /// then returning it (to the Service)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RuleNameDTO> GetSingleRuleName(int id)
        {
            var ruleName = await _context.RuleName.SingleOrDefaultAsync(rn => rn.Id == id);
            if (ruleName is null)
            {
                return null;
            }
            return RuleNameToDTO(ruleName);

        }

        /// <summary>
        /// Method that CREATEs a RuleName and save it to the database
        /// </summary>
        /// <param name="ruleNameDTO"></param>
        /// <returns></returns>
        public async Task<List<RuleNameDTO>> CreateRuleName(RuleNameDTO ruleNameDTO)
        {
            // Create new RuleName 
            var ruleName = new RuleName
            {
                Id = ruleNameDTO.Id,
                Name = ruleNameDTO.Name,
                Description = ruleNameDTO.Description
            };

            // Save new RuleName in the database
            _context.RuleName.Add(ruleName);
            await _context.SaveChangesAsync();

            // return the entire list of RuleNames with the newly added RuleName
            var RuleNamesAfterAddition = GetAllRuleNames();
            return await RuleNamesAfterAddition;
        }

        /// <summary>
        /// Method that UPDATEs a RuleName with a given Id in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ruleNameDTO"></param>
        /// <returns></returns>
        public async Task<RuleNameDTO> UpdateRuleName(int id, RuleNameDTO ruleNameDTO)
        {
            var ruleNameToUpdate = _context.RuleName.Find(id);
            if (ruleNameToUpdate is null)
            {
                return null;
            }

            ruleNameToUpdate.Name = ruleNameDTO.Name;
            ruleNameToUpdate.Description = ruleNameDTO.Description;

            await _context.SaveChangesAsync();

            // Return the updated RuleName
            var result = RuleNameToDTO(ruleNameToUpdate);
            return result;
        }

        /// <summary>
        /// Method to DELETE a RuleName with a given Id from the database
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<RuleNameDTO>> DeleteRuleName(int id)
        {
            RuleName ruleNameToDelete = _context.RuleName.Find(id);
            if (ruleNameToDelete is null)
            {
                return null;
            }
            _context.RuleName.Remove(ruleNameToDelete);
            _context.SaveChanges();

            // Return the entire list of RuleNames without the newly removed RuleName
            var RuleNamesAfterDeletion = GetAllRuleNames();
            return await RuleNamesAfterDeletion;
        }

        /// <summary>
        /// Method that converts a RuleName into a RuleNameDTO
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        private static RuleNameDTO RuleNameToDTO(RuleName ruleName) =>
        new RuleNameDTO
        {
            Id = ruleName.Id,
            Name = ruleName.Name,
            Description = ruleName.Description
        };
    }
}
