import { AppState } from "../AppState";
import { InstructionStep } from "../models/InstructionStep";
import { api } from "./AxiosService";

class InstructionStepService {
  async getInstructions(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/instructions`);
    AppState.instructions = res.data.map(step => new InstructionStep(step));
  }
  async addStep(stepData) {
    const res = await api.post('api/instructions', stepData);
    AppState.instructions.push(new InstructionStep(res.data));
  }

  async removeStep(stepId) {
    const res = await api.delete('api/instructions/' + stepId);
    AppState.instructions = AppState.instructions.filter(step => step.id != stepId);
  }

  async updateStep(stepData) {
    const res = await api.put('api/instructions/' + stepData.id, stepData);
    const index = AppState.instructions.findIndex(step => step.id == stepData.id);
    AppState.instructions.splice(index, 1, new InstructionStep(res.data));
  }
}

export const instructionStepService = new InstructionStepService();