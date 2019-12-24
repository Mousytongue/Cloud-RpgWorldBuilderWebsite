using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using rpgworldbuilder.Models;

namespace rpgworldbuilder.Controllers
{
    public class MapsController : ApiController
    {
        private rpgworldbuilderDatabaseEntities db = new rpgworldbuilderDatabaseEntities();

        // GET: api/Maps
        public IQueryable<Map> GetMaps()
        {
            return db.Maps;
        }

        // GET: api/Maps/5
        [ResponseType(typeof(Map))]
        public async Task<IHttpActionResult> GetMap(int id)
        {
            Map map = await db.Maps.FindAsync(id);
            if (map == null)
            {
                return NotFound();
            }

            return Ok(map);
        }

        // PUT: api/Maps/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMap(int id, Map map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != map.MapID)
            {
                return BadRequest();
            }

            db.Entry(map).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Maps
        [ResponseType(typeof(Map))]
        public async Task<IHttpActionResult> PostMap(Map map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Maps.Add(map);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = map.MapID }, map);
        }

        // DELETE: api/Maps/5
        [ResponseType(typeof(Map))]
        public async Task<IHttpActionResult> DeleteMap(int id)
        {
            Map map = await db.Maps.FindAsync(id);
            if (map == null)
            {
                return NotFound();
            }

            db.Maps.Remove(map);
            await db.SaveChangesAsync();

            return Ok(map);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MapExists(int id)
        {
            return db.Maps.Count(e => e.MapID == id) > 0;
        }
    }
}