# Стратегия

PYTHON
```
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
```

UML
```
@startuml
class DrinkStrategy {
    +prepare(): void
}

class TeaStrategy {
    +prepare(): void
}

class CoffeeStrategy {
    +prepare(): void
}

class Cafe {
    -drink_strategy: DrinkStrategy
    +__init__(drink_strategy: DrinkStrategy)
    +order_drink(): void
    +set_drink_strategy(drink_strategy: DrinkStrategy): void
}

DrinkStrategy <|-down- TeaStrategy
DrinkStrategy <|-down- CoffeeStrategy
Cafe o-> DrinkStrategy
@enduml
```

![str](https://github.com/Vanifatov/tmp/assets/90778174/8f3c9665-7f14-4879-b5be-f317012285c3)


# Шаблонный метод

PYTHON
```
from abc import ABC, abstractmethod

# Абстрактный класс для приготовления напитков
class DrinkMaker(ABC):
    
    def prepare(self):
        self.boil_water()
        self.brew()
        self.pour()
        if self.customer_wants_condiments():
            self.add_condiments()
    
    def boil_water(self):
        print("Кипятим воду")
    
    @abstractmethod
    def brew(self):
        pass
    
    def pour(self):
        print("Наливаем напиток в чашку")
    
    @abstractmethod
    def add_condiments(self):
        pass
    
    # Хук, который можно переопределить в подклассах
    def customer_wants_condiments(self):
        return True

# Конкретные классы для приготовления различных напитков
class TeaMaker(DrinkMaker):
    
    def brew(self):
        print("Завариваем чай")
    
    def add_condiments(self):
        print("Добавляем лимон")

class CoffeeMaker(DrinkMaker):
    
    def brew(self):
        print("Готовим кофе")
    
    def add_condiments(self):
        print("Добавляем молоко")

    def customer_wants_condiments(self):
        answer = input("Вы хотите добавить молоко? (y/n)")
        return True if answer.lower() == "y" else False
```

UML
```
@startuml
abstract class DrinkMaker {
    +prepare(): void
    +boil_water(): void
    +brew(): void
    +pour(): void
    +add_condiments(): void
    +customer_wants_condiments(): boolean
}

class TeaMaker {
    +brew(): void
    +add_condiments(): void
}

class CoffeeMaker {
    +brew(): void
    +add_condiments(): void
    +customer_wants_condiments(): boolean
}

DrinkMaker <|-down- TeaMaker
DrinkMaker <|-down- CoffeeMaker
@enduml
```

![templ](https://github.com/Vanifatov/tmp/assets/90778174/b73a8c40-6d8e-4930-8010-9bce38d09004)

