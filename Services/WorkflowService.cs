using System.Text.Json;
using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public class WorkflowService : IWorkflowService
{
    public string GenerateJson(WorkflowModel workflow)
    {
        var activities = new List<object>();
        var connections = new List<object>();

        var triggerId = "trigger";
        activities.Add(new
        {
            id = triggerId,
            type = workflow.Trigger.ActivityType,
            condition = workflow.Trigger.Condition
        });

        if (workflow.Steps.Any())
            connections.Add(new { source = triggerId, target = workflow.Steps.First().Id });

        foreach (var step in workflow.Steps)
        {
            activities.Add(new
            {
                id = step.Id,
                type = step.ActivityType,
                delay = step.DelaySeconds
            });

            foreach (var next in step.NextStepIds)
                connections.Add(new { source = step.Id, target = next });
        }

        var data = new { name = workflow.Name, activities, connections };
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }
}
