using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Services.FlashCards;
using ThoughtzLand.Core.Services;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace ThoughtzLand.Api.Controllers
{
    [Route("api/flashcard")]
	[ApiController]
	[Authorize]
	public class FlashCardController : ControllerBase
	{
		private readonly FlashCardService service;

		public FlashCardController(FlashCardService service) 
		{
			this.service = service;
		}

		//[HttpGet("ofnode")]
		//public IActionResult Get(int nodeid, DateTime date)
		//{
		//	return Ok(service.Get(nodeid, date));
		//}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(service.GetSingle(id));
		}

		//[HttpPatch("answer/update-string")]
		//public IActionResult CardAnswerUpdateProperty([FromBody] UpdatePropertyDto<string> prop)
		//{
		//	service.CardAnswerUpdateProperty(prop.id, prop.name, prop.value);
		//	return Ok();
		//}

		//[HttpPatch("update-string")]
		//public IActionResult CardUpdateProperty([FromBody] UpdatePropertyDto<string> prop)
		//{
		//	service.CardUpdateProperty(prop.id, prop.name, prop.value);
		//	return Ok();
		//}

		[HttpPost("answer")]
		public IActionResult Add([FromBody] CreateFlashCardAnswerDto dto)
		{
			return Ok(service.AddAnswer(dto));
		}

		[HttpPost]
		public IActionResult Add([FromBody] CreateFlashCardDto dto) 
		{
			return Ok(service.Add(dto));
		}

		[HttpPut]
		public IActionResult Update([FromBody] UpdateFlashCardDto dto)
		{
			return Ok(service.UpdateFlashCard(dto));
		}

		[HttpPut("answer")]
		public IActionResult UpdateCardAnswer([FromBody] UpdateCardAnswerDto dto)
		{
			service.UpdateCardAnswer(dto);
			return Ok();
		}

		[HttpDelete("answer/{id}")]
		public IActionResult Delete(int id)
		{
			service.DeleteCardAnswer(id);
			return Ok();
		}

		[HttpGet("playing/ofnode/{nodeId}")]
		//public IActionResult GetPlayingCards(int nodeId, DateTime date)
		public IActionResult GetPlayingCards(int nodeId)
		{
			//return Ok(service.GetPlayingCards(nodeId, date));
			return Ok(service.GetPlayingCards(nodeId));
		}

        /// <summary>
        /// Частично обновляет FlashCard используя Optional паттерн
        /// </summary>
        /// <param name="patchDto">DTO с опциональными полями для обновления</param>
        /// <returns>Обновленная карточка</returns>
        [HttpPatch("patch")]
        public async Task<ActionResult<FlashCard>> PatchFlashCard([FromBody] PatchFlashCardDto patchDto)
        {
            try
            {
                // Валидация модели
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Получаем существующую сущность
                var existingCard = flashCardRepo.Get(patchDto.Id);
                if (existingCard == null)
                {
                    return NotFound($"FlashCard with ID {patchDto.Id} not found");
                }

                // Применяем частичное обновление
                patchDto.ApplyPatch(existingCard, patchDto);

                // Сохраняем изменения через репозиторий
                var updatedCard = flashCardRepo.Update(new UpdateFlashCardDto
                {
                    id = existingCard.id,
                    question = existingCard.question,
                    description = existingCard.description,
                    languageId = existingCard.languageId
                });

                return Ok(updatedCard);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        /// <summary>
        /// Частично обновляет FlashCard используя JSON Patch RFC 6902
        /// </summary>
        /// <param name="jsonPatchDto">JSON Patch операции</param>
        /// <returns>Обновленная карточка</returns>
        [HttpPatch("jsonpatch")]
        public async Task<ActionResult<FlashCard>> JsonPatchFlashCard([FromBody] JsonPatchFlashCardDto jsonPatchDto)
        {
            try
            {
                // Валидация модели
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Получаем существующую сущность
                var existingCard = flashCardRepo.Get(jsonPatchDto.Id);
                if (existingCard == null)
                {
                    return NotFound($"FlashCard with ID {jsonPatchDto.Id} not found");
                }

                // Применяем JSON Patch операции
                jsonPatchDto.ApplyJsonPatch(existingCard);

                // Сохраняем изменения через репозиторий
                var updatedCard = flashCardRepo.Update(new UpdateFlashCardDto
                {
                    id = existingCard.id,
                    question = existingCard.question,
                    description = existingCard.description,
                    languageId = existingCard.languageId
                });

                return Ok(updatedCard);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        /// <summary>
        /// Частично обновляет FlashCard используя динамический объект (простой подход)
        /// </summary>
        /// <param name="id">ID карточки</param>
        /// <param name="patch">Динамический объект с полями для обновления</param>
        /// <returns>Обновленная карточка</returns>
        [HttpPatch("{id}/simple")]
        public async Task<ActionResult<FlashCard>> SimplePatchFlashCard(int id, [FromBody] dynamic patch)
        {
            try
            {
                // Получаем существующую сущность
                var existingCard = flashCardRepo.Get(id);
                if (existingCard == null)
                {
                    return NotFound($"FlashCard with ID {id} not found");
                }

                // Применяем простое частичное обновление
                existingCard.ApplyPatch(patch);

                // Сохраняем изменения через репозиторий
                var updatedCard = flashCardRepo.Update(new UpdateFlashCardDto
                {
                    id = existingCard.id,
                    question = existingCard.question,
                    description = existingCard.description,
                    languageId = existingCard.languageId
                });

                return Ok(updatedCard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
	}
}
