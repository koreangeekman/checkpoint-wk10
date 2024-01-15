

namespace allspice.Models;

public class InstructionStepRepo
{
  private readonly IDbConnection db;

  public InstructionStepRepo(IDbConnection _db)
  {
    db = _db;
  }

  internal List<InstructionStep> GetInstructions(int recipeId)
  {
    string sql = "SELECT * FROM instructions WHERE recipeId = @RecipeId;";
    return db.Query<InstructionStep>(sql, new { recipeId }).ToList();
  }

  internal InstructionStep GetStepById(int stepId)
  {
    string sql = "SELECT * FROM instructions WHERE id = @StepId;";
    return db.QueryFirstOrDefault<InstructionStep>(sql, new { stepId });
  }

  // internal int GetStepCountForRecipe(int recipeId)
  // {
  //   string sql = @"SELECT MAX(position) AS stepCount FROM instructions WHERE recipeId = @RecipeId;";
  //   NewPosition newPosition = db.QueryFirstOrDefault(sql, new { recipeId });
  //   return newPosition.StepCount;
  // }

  internal InstructionStep CreateStep(InstructionStep stepData)
  {
    // SELECT COUNT(*) AS current_step_count FROM instructions GROUP BY recipeId;
    string sql = @"
        INSERT INTO
            instructions(position, body, recipeId)
        SELECT
            MAX(position) + 1,
            @Body,
            @RecipeId
        FROM instructions
        WHERE recipeId = @RecipeId;

        SELECT * FROM instructions WHERE id = LAST_INSERT_ID();";
    return db.QueryFirstOrDefault<InstructionStep>(sql, stepData);
  }

  internal InstructionStep UpdateStep(InstructionStep stepData)
  {
    string sql = @"UPDATE instructions SET
        position = @Position,
        body = @Body
        WHERE id = @Id;";
    return db.QueryFirstOrDefault<InstructionStep>(sql, stepData);
  }

  internal void RemoveStep(int stepId)
  { db.Query("DELETE FROM instructions WHERE id = @StepId;", new { stepId }); }

}