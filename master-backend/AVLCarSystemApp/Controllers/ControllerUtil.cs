using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.DataAccessInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace AVLCarSystemApp.Controllers
{
  public class ControllerUtil
  {
    private class ErrorMessage
    {
      public string Message { get; set; }
      public object Item { get; set; }
    }

    public static IActionResult Delete<T, TM>(Controller context,
      IRepository<T> repo,
      T entityDto,
      TM entity) where T : class
    {
      try
      {
        if (repo.CanRemove(entityDto))
          repo.Remove(entityDto);
        else
          return context.BadRequest(new ErrorMessage()
          {
            Message = "Entity can't be deleted!",
            Item = entity
          });
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }

      return context.Ok(entity);
    }

    public static IActionResult Delete<T, TM>(Controller context,
      IRepository<T> repo,
      IMapper mapper,
      Expression<Func<T, bool>> findPredicate) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      T entityDto;
      try
      {
        entityDto = repo.Find(findPredicate).FirstOrDefault();
        if (entityDto == null)
          return context.NotFound();
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }

      TM entityModel = mapper.Map<T, TM>(entityDto);
      return Delete<T, TM>(context, repo, entityDto, entityModel);
    }

    public static IActionResult Put<T, TM>(Controller context,
      IRepository<T> repo,
      IMapper mapper,
      TM model,
      Expression<Func<T, bool>> findPredicate) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      try
      {
        T entityDto = mapper.Map<TM, T>(model);
        if (!findPredicate.Compile()(entityDto))
          return context.BadRequest();

        repo.Update(entityDto);
      }
      catch (DbUpdateConcurrencyException e)
      {
        var exist = repo.Find(findPredicate).Any();
        if (!exist)
          return context.NotFound();

        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }

      return context.NoContent();
    }

    public static IActionResult Post<T, TM>(Controller context,
      IRepository<T> repo,
      IMapper mapper,
      TM model,
      Expression<Func<TM, object>> routeValues) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      T entityDto = mapper.Map<TM, T>(model);
      try
      {
        repo.Add(entityDto);
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }

      TM trackedModel = mapper.Map<T, TM>(entityDto);
      return context.CreatedAtAction("Get", routeValues.Compile()(trackedModel), trackedModel);
    }


    public static IActionResult Get<T, TM>(Controller context,
      IRepository<T> repo,
      IMapper mapper,
      long id) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      try
      {
        T entityDto = repo.Get(id);
        if (entityDto == null)
          return context.NotFound($"Item with id {id} does not exist!");

        TM entityModel = mapper.Map<T, TM>(entityDto);
        return context.Ok(entityModel);
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    public static IActionResult GetAll<T, TM>(Controller context, IRepository<T> repo, IMapper mapper) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      try
      {
        IEnumerable<T> entitiesDto = repo.GetAll();
        IEnumerable<TM> models = mapper.Map<IEnumerable<T>, IEnumerable<TM>>(entitiesDto);
        return context.Ok(models);
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    public static IActionResult GetFiltered<T, TM>(Controller context,
      IRepository<T> repo,
      IMapper mapper,
      Expression<Func<T, bool>> filter) where T : class
    {
      if (!context.ModelState.IsValid)
        return context.BadRequest(context.ModelState);

      try
      {
        IEnumerable<T> entitiesDto = repo.Find(filter);
        IEnumerable<TM> models = mapper.Map<IEnumerable<T>, IEnumerable<TM>>(entitiesDto);
        return context.Ok(models);
      }
      catch (Exception e)
      {
        return context.StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
  }
}

