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