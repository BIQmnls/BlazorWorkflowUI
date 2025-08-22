using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public interface IWorkflowService
{
    string GenerateJson(WorkflowModel workflow);
    void Save(WorkflowModel workflow);
    WorkflowModel? Load(string name);
}
