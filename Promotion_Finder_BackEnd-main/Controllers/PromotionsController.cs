using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SubwayPromotion.Models;
using SubwayPromotion.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubwayPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly PromotionService _promotionService;
        private readonly TermsService _termsService;
        private readonly LocationService _locationService;
        private readonly IMapper mapper;

        public PromotionsController(PromotionService promotionService, TermsService termsService, LocationService locationService, IMapper mapper)
        {
            _promotionService = promotionService;
            _termsService = termsService;
            _locationService = locationService;
            this.mapper = mapper;
        }

        // GET: api/Promotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDTO>>> GetPromotions()
        {
            var promotionsDto = await _promotionService.GetAll();
            return Ok(promotionsDto);
        }


        // GET: api/Promotions/{country}
        [HttpGet("{country}")]
        public async Task<ActionResult<IEnumerable<PromotionUI>>> GetPromotionsByCountry(string country)
        {
            try
            {
                List<PromotionUI> promotions = new List<PromotionUI>();
                var promotionsDto = await _promotionService.GetByCountry(country);
                if (promotionsDto == null)
                {
                    return NotFound();
                }
                foreach (var p in promotionsDto)
                {
                    PromotionUI promotion = new PromotionUI();
                    promotion.PromotionID = p.Id.ToString();
                    promotion.PromotionName = p.PromotionName;
                    var terms = (await _termsService.GetAllByIds(p.TermsOfUse)).ToList();
                    promotion.TermsOfUse = terms.Select(x => x.TermConditon).ToList();
                    promotion.TermsOfUse.Add("Valid from " + p.ValidityStart.ToString() + " to " + p.ValidityEnd.ToString());
                    var locations = (await _locationService.GetByLocationIds(p.Locations)).ToList();
                    promotion.Locations = locations.Select(x => x.Address).ToList();
                    promotions.Add(promotion);
                }
                return promotions;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        // POST: api/Promotions
        [HttpPost]
        public async Task<ActionResult<PromotionDTO>> PostPromotion(Promotion promotion)
        {
            var promotionDto = mapper.Map<PromotionDTO>(promotion);
            await _promotionService.Add(promotionDto);
            return Created("", promotion);
        }

        // DELETE: api/Promotions/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePromotion(string id)
        {
            await _promotionService.Delete(id);
            return NoContent();
        }

        // PUT: api/Promotions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPromotion(string id, Promotion promotion)
        {
            var promotionDto = mapper.Map<PromotionDTO>(promotion);
            await _promotionService.Update(promotionDto);
            return NoContent();
        }

        // [HttpPost("addTerms")]
        // public async Task<ActionResult> AddTerms(Term term)
        // {
        //     var termDTO = mapper.Map<TermDTO>(term);
        //     await _termsService.Add(termDTO);
        //     return Ok();
        // }
        [HttpGet("getTerms")]
        public async Task<ActionResult<IEnumerable<TermDTO>>> GetTerms()
        {
            var terms = await _termsService.GetAll();
            return Ok(terms);
        }

    }
}