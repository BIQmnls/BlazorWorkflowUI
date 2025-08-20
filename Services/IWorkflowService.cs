using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public interface IWorkflowService
{
    string GenerateJson(WorkflowModel workflow);
}
