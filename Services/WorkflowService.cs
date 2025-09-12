using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public class WorkflowService
{
    private readonly List<Workflow> _workflows = new();

    public Task<List<Workflow>> GetWorkflowsAsync() => Task.FromResult(_workflows);

    public Task<Workflow?> GetWorkflowAsync(string id) =>
        Task.FromResult(_workflows.FirstOrDefault(w => w.Id == id));

    public Task SaveWorkflowAsync(Workflow workflow)
    {
        var index = _workflows.FindIndex(w => w.Id == workflow.Id);
        if (index >= 0)
            _workflows[index] = workflow;
        else
            _workflows.Add(workflow);
        return Task.CompletedTask;
    }
}
