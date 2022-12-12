using backend.Muscles;
namespace backend.Exercises;


public interface IConditionExcercise
{
    bool Puede_Hacer(IMusculos a, Conditions conditions);
}
