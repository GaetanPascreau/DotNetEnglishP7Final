using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi3.Domain.DTO;

namespace WebApi3.Repositories
{
    public interface IRuleNameRepository
    {
        Task<List<RuleNameDTO>> GetAllRuleNames();
        Task<RuleNameDTO> GetSingleRuleName(int id);
        Task<List<RuleNameDTO>> CreateRuleName(RuleNameDTO ruleNameDTO);
        Task<RuleNameDTO> UpdateRuleName(int id, RuleNameDTO ruleNameDTO);
        Task<List<RuleNameDTO>> DeleteRuleName(int id);
    }
}
