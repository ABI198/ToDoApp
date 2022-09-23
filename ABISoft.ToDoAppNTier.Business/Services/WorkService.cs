using ABISoft.ToDoAppNTier.Business.Interfaces;
using ABISoft.ToDoAppNTier.Business.ValidationRules;
using ABISoft.ToDoAppNTier.Common.ResponseObjects;
using ABISoft.ToDoAppNTier.DataAccess.UnitofWork;
using ABISoft.ToDoAppNTier.Dtos.Interfaces;
using ABISoft.ToDoAppNTier.Dtos.WorkDtos;
using ABISoft.ToDoAppNTier.Entities.Concrete;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _workCreateDtoValidator;
        private readonly IValidator<WorkUpdateDto> _workUpdateDtoValidator;
        public WorkService(IUnitOfWork uof, IMapper mapper, IValidator<WorkCreateDto> workCreateDtoValidator, IValidator<WorkUpdateDto> workUpdateDtoValidator)
        {
            _uof = uof;
            _mapper = mapper;
            _workCreateDtoValidator = workCreateDtoValidator;
            _workUpdateDtoValidator = workUpdateDtoValidator;
        }
        public async Task<IResponse<WorkCreateDto>> AddAsync(WorkCreateDto dto)
        {
            var validationResult = _workCreateDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var entity = _mapper.Map<Work>(dto);
                await _uof.GetRepository<Work>().AddAsync(entity);
                await _uof.SaveChangesAsync();
                return new Response<WorkCreateDto>(ResponseType.Success, dto); 
            }
            else
            {
                List<CustomValidationError> validationErrors = new List<CustomValidationError>();
                foreach (var error in validationResult.Errors)
                {
                    validationErrors.Add(new CustomValidationError()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validationErrors);
            }
        }
        public async Task<IResponse<List<WorkShowDto>>> GetAllAsync()
        {
            var workList = await _uof.GetRepository<Work>().GetAllAsync();
            var workShowDtoList = _mapper.Map<List<WorkShowDto>>(workList); 
            return new Response<List<WorkShowDto>>(ResponseType.Success, workShowDtoList);
        }
        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var work = await _uof.GetRepository<Work>().GetByFilterAsync(x => x.Id == id);
            if (work != null)
            {
                var workSelectedDto = _mapper.Map<IDto>(work);
                return new Response<IDto>(ResponseType.Success, workSelectedDto);
            }
            else
                return new Response<IDto>(ResponseType.NotFound, "Data with id = " + id + "could not be found.");
        }
        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uof.GetRepository<Work>().GetByFilterAsync(x => x.Id == id);
            if (removedEntity != null)
            {
                _uof.GetRepository<Work>().Remove(removedEntity);
                await _uof.SaveChangesAsync();
                return new Response(ResponseType.Success);
            }
            else
                return new Response(ResponseType.NotFound, "Data with id = " + id + "could not be found.");
        }
        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            var validationResult = _workUpdateDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var entity = await _uof.GetRepository<Work>().GetByIdAsync(dto.Id); //Tracking is needed for entity (State -> Unchanged)
                if (entity != null)
                {
                    var updatedEntity = _mapper.Map<Work>(dto);
                    _uof.GetRepository<Work>().Update(entity, updatedEntity);
                    await _uof.SaveChangesAsync();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, "Data with id = " + dto.Id + "could not be found.");
            }
            else
            {
                List<CustomValidationError> validationErrors = new List<CustomValidationError>();
                foreach (var error in validationResult.Errors)
                {
                    validationErrors.Add(new CustomValidationError()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertyName = error.PropertyName
                    });
                }
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, validationErrors);
            }
        }
    }
} 
