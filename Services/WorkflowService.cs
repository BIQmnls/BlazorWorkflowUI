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
            condition = workflow.Trigger.Condition == "NoCondition" ? null : workflow.Trigger.Condition
        });

        var previousId = triggerId;
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

            connections.Add(new { source = previousId, target = step.Id });
            previousId = step.Id;
        }

        var data = new { name = workflow.Name, activities, connections };
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }
}
