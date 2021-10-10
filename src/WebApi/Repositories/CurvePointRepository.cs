using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<CurvePointRepository> _logger;

        public CurvePointRepository(LocalDbContext dbContext, ILogger<CurvePointRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<CurvePoint>> Add(CurvePoint curvePoint)
        {
            if (curvePoint == null) throw new ArgumentNullException(nameof(curvePoint));

            _logger.LogInformation("Adding new point list {@CurvePoint}", curvePoint);
            _dbContext.CurvePoints.Add(curvePoint);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("CurvePoint Saved {@CurvePoint}", curvePoint);

            return await _dbContext.CurvePoints.ToListAsync();
        }

        public async Task<IReadOnlyCollection<CurvePoint>> Update(int id, CurvePoint curvePoint)
        {
            if (curvePoint == null) throw new ArgumentNullException(nameof(curvePoint));

            var point = await _dbContext.CurvePoints.FirstOrDefaultAsync(b => b.Id == id);
            if (point == null) throw new BidNotFoundException();

            _logger.LogInformation("Updating curve point {@Point} to {@CurvePoint}", point, curvePoint);

            point.CreationDate = curvePoint.CreationDate;
            point.CurveId = curvePoint.CurveId;
            point.AsOfDate = curvePoint.AsOfDate;
            point.Term = curvePoint.Term;
            point.Value = curvePoint.Value;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("CurvePoint Saved {@CurvePoint}", curvePoint);

            return await _dbContext.CurvePoints.ToListAsync();
        }

        public async Task<IReadOnlyCollection<CurvePoint>> GetAll()
        {
            _logger.LogInformation("Listing points in the databaase");
            var points = await _dbContext.CurvePoints.ToListAsync();
            return points;
        }
        public async Task<CurvePoint> Get(int id)
        {
            _logger.LogInformation("Getting point with id \"{Id}\"", id);

            var point = await _dbContext.CurvePoints.FirstOrDefaultAsync(b => b.Id == id);
            return point == null ? throw new BidNotFoundException() : point;
        }
        public async Task<IReadOnlyCollection<CurvePoint>> Delete(int id)
        {
            _logger.LogInformation("Deleting point with id \"{Id}\"", id);

            var point = await _dbContext.CurvePoints.FirstOrDefaultAsync(b => b.Id == id);
            if (point == null) throw new BidNotFoundException();

            _dbContext.CurvePoints.Remove(point);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Bid Deleted {@Point}", point);

            return await _dbContext.CurvePoints.ToListAsync();
        }
    }
}
