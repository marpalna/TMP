from abc import ABC, abstractmethod

# Абстрактный класс стратегии
class DrinkStrategy(ABC):
    
    @abstractmethod
    def prepare(self):
        pass
    
# Конкретные классы стратегии
class TeaStrategy(DrinkStrategy):
    
    def prepare(self):
        print("Приготовить чай")
    
class CoffeeStrategy(DrinkStrategy):
    
    def prepare(self):
        print("Приготовить кофе")
    
# Контекст
class Cafe:
    
    def __init__(self, drink_strategy):
        self.drink_strategy = drink_strategy
    
    def order_drink(self):
        self.drink_strategy.prepare()
        
    def set_drink_strategy(self, drink_strategy):
        self.drink_strategy = drink_strategy

def main():
    tea_strategy = TeaStrategy()
    coffee_strategy = CoffeeStrategy()

    cafe = Cafe(tea_strategy)
    cafe.order_drink() # Приготовить чай

    cafe.set_drink_strategy(coffee_strategy)
    cafe.order_drink() # Приготовить кофе

if __name__ == '__main__':
    main()