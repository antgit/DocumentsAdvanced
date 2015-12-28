using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using BusinessObjects.Windows;
using System.Linq;
namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        public static bool ValidateRuleSet<T>(this T value, string key) where T: ICoreObject
        {
            
            Ruleset ruleset = value.Workarea.Cashe.GetCasheData<Ruleset>().ItemCode<Ruleset>(key);
            if (ruleset == null) return true;

            Type targetType = value.GetType();
            RuleSet ruleSetToValidate = ruleset.DeserializeRuleSet();
            RuleEngine engine = new RuleEngine(ruleSetToValidate, targetType);
            engine.Execute(value);
            return (value.Errors.Count == 0);
        }
        public static bool ValidateRuleSet<T>(this T value) where T: ICoreObject
        {
            Type targetType = value.GetType();
            string fulltypename = targetType.FullName;
            IEnumerable<Ruleset> collRules = value.Workarea.GetCollection<Ruleset>().Where(s => s.ActivityName == fulltypename && s.StateId == 1 && s.KindValue==1);
            //if (collRules == null) return true;

            foreach (Ruleset rule in collRules)
            {
                RuleSet ruleSetToValidate = rule.DeserializeRuleSet();
                if (ruleSetToValidate != null)
                {
                    RuleEngine engine = new RuleEngine(ruleSetToValidate, targetType);
                    engine.Execute(value);
                    if (value.Errors.Count > 0)
                        return false;
                }
            }

            return true;
        }
        public static bool ValidateRuleSetView<T>(this T value) where T: IDocumentView
        {
            Type targetType = value.GetType();
            string fulltypename = targetType.FullName;
            IEnumerable<Ruleset> collRules = value.Workarea.GetCollection<Ruleset>().Where(s => s.ActivityName == fulltypename && s.StateId == 1 && s.KindValue == 1);
            //if (collRules == null) return true;

            foreach (Ruleset rule in collRules)
            {
                RuleSet ruleSetToValidate = rule.DeserializeRuleSet();
                if (ruleSetToValidate != null)
                {
                    RuleEngine engine = new RuleEngine(ruleSetToValidate, targetType);

                    engine.Execute(value);
                    if (value.MainDocument.Errors.Count > 0)
                        return false;
                }
            }

            return true;
        }
        /*
        internal static bool ValidateRuleSet(RuleSet ruleSetToValidate, Type targetType, bool promptForContinue)
        {
            if (ruleSetToValidate == null)
            {
                MessageBox.Show("No RuleSet selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }

            if (targetType == null)
            {
                MessageBox.Show("No Type is associated with the RuleSet.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }

            RuleValidation ruleValidation = new RuleValidation(targetType, null);
            ruleSetToValidate.Validate(ruleValidation);
            if (ruleValidation.Errors.Count == 0)
            {
                return true;
            }
            else
            {
                ValidationErrorsForm validationDialog = new ValidationErrorsForm();
                validationDialog.SetValidationErrors(ruleValidation.Errors);
                validationDialog.PromptForContinue = promptForContinue;
                validationDialog.ErrorText = "The RuleSet failed validation.  Ensure that the selected Type has the public members referenced by this RuleSet.  Note that false errors may occur if you are referencing different copies of an assembly with the same strong name.";
                if (promptForContinue)
                    validationDialog.ErrorText += "  Select Continue to proceed or Cancel to return.";
                
                validationDialog.ShowDialog();

                if (!promptForContinue)
                    return false;
                else
                    return validationDialog.ContinueWithChange;
            }
        }
         */
    }
}
