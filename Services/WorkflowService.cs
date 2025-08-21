using System.Text.Json;
using BlazorWorkflowUI.Models;

namespace BlazorWorkflowUI.Services;

public class WorkflowService : IWorkflowService
{
    public string GenerateJson(WorkflowModel workflow)
    {
        var activities = new List<object>();
        var connections = new List<object>();

        string? previousId = null;
        foreach (var step in workflow.Steps)
        {
            activities.Add(new
            {
                id = step.Id,
                type = step.ActivityType,
                delay = step.ActivityType == "WaitForDocuments" ? step.DelaySeconds : null,
                condition = step.Condition == "NoCondition" ? null : step.Condition,
                elseType = step.ElseActivityType,
                elseDelay = step.ElseActivityType == "WaitForDocuments" ? step.ElseDelaySeconds : null
            });

            if (previousId != null)
            {
                connections.Add(new { source = previousId, target = step.Id });
            }
            previousId = step.Id;
        }

        var data = new
        {
            name = workflow.Name,
            trigger = workflow.Trigger.ActivityType,
            activities,
            connections
        };
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }
}
