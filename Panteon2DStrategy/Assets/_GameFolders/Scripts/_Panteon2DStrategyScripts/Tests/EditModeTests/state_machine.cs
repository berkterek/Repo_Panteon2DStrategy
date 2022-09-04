using NSubstitute;
using NUnit.Framework;
using Panteon2DStrategy.Abstracts.StateMachineObjects;
using Panteon2DStrategy.StateMachineObjects;

namespace States
{
    public class state_machine
    {
        [Test]
        public void create_state_machine_and_set_default_state_state1_tick_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            stateMachine.Tick();
            
            state1.Received().Tick();
        }
        
        [Test]
        public void create_state_machine_and_set_default_state_state1_fixed_tick_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            stateMachine.FixedTick();
            
            state1.Received().FixedTick();
        }
        
        [Test]
        public void create_state_machine_and_not_set_state_state2_tick_not_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            stateMachine.Tick();
            
            state2.DidNotReceive().Tick();
        }
        
        [Test]
        public void create_state_machine_and_not_set__state_state2_fixed_tick_not_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            stateMachine.FixedTick();
            
            state2.DidNotReceive().FixedTick();
        }

        [Test]
        public void set_transformer_ordered_when_condition_true_and_current_state_equal_to_state_change_state1_to_state2()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);

            bool conditionValue = false;
            stateMachine.SetOrderedState(state1,state2, () => conditionValue);

            conditionValue = true;
            
            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();    
            }
            
            state2.Received().Tick();
        }
        
        [Test]
        public void set_transformer_ordered_when_condition_false_not_change_state1_to_state2()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);

            bool conditionValue = false;
            stateMachine.SetOrderedState(state1,state2, () => conditionValue);

            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();    
            }
            
            state2.DidNotReceive().Tick();
        }
        
        [Test]
        public void set_transformer_ordered_when_condition_true_but_to_state_not_equal_to_state_not_change_state3_to_state2()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IState state3 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);

            bool conditionValue = false;
            stateMachine.SetOrderedState(state3,state2, () => conditionValue);

            conditionValue = true;
            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();    
            }
            
            state2.DidNotReceive().Tick();
        }

        [Test]
        public void set_transformer_any_when_condition_true_and_change_state1_to_state2()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            
            IStateMachine stateMachine = new StateMachine(state1);
            
            bool conditionValue = false;
            stateMachine.SetAnyState(state2, () => conditionValue);
            conditionValue = true;

            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();
            }
            
            state2.Received().Tick();
        }
        
        [Test]
        public void set_transformer_any_when_condition_false_and_not_change_state1_to_state2()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            
            IStateMachine stateMachine = new StateMachine(state1);
            
            bool conditionValue = false;
            stateMachine.SetAnyState(state2, () => conditionValue);

            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();
            }
            
            state2.DidNotReceive().Tick();
        }

        [Test]
        public void set_first_state_and_start_state_method_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            state1.Received().StartState();
        }
        
        [Test]
        public void set_first_state_when_state_changed_end_state_triggered()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            bool conditionValue = false;
            stateMachine.SetOrderedState(state1,state2, () => conditionValue);
            conditionValue = true;

            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();
            }
            
            state1.Received().EndState();
        }
        
        [Test]
        public void set_first_state_when_state_changed_new_state_triggered_start_state_method()
        {
            IState state1 = Substitute.For<IState>();
            IState state2 = Substitute.For<IState>();
            IStateMachine stateMachine = new StateMachine(state1);
            
            bool conditionValue = false;
            stateMachine.SetOrderedState(state1,state2, () => conditionValue);
            conditionValue = true;

            for (int i = 0; i < 5; i++)
            {
                stateMachine.Tick();
            }
            
            state2.Received().StartState();
        }
    }
}
