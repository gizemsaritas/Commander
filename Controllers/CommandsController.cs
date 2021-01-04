using System.ComponentModel.Design;
using System.Collections.Generic;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using Commander.Data;
using AutoMapper;
using Commander.Dtos;

namespace Commander.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //private readonly MockCommanderRepo _repository=new MockCommanderRepo();
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository,IMapper mapper)
        {
            _repository= repository;
            _mapper=mapper;
        }
        
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems=_repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        //GET api/commands/{id}
        [HttpGet("{id}",Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem=_repository.GetCommandById(id);
            if(commandItem!=null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/IMenuCommandService     
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto){
            var commandModel=_mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto=_mapper.Map<CommandReadDto>(commandModel);
            
            //return Ok(commandReadDto);
            return CreatedAtRoute(nameof(GetCommandById),new {Id=commandReadDto.Id},commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]  
        public ActionResult UpdateCommand(int id,CommandUpdateDto commandUpdateDto){
            var commandModelFromRepo=_repository.GetCommandById(id);
            if(commandModelFromRepo==null){
                return NotFound();
            }
            _mapper.Map(commandUpdateDto,commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        } 
    }

}