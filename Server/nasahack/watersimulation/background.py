from threading import Thread
from time import sleep
from watersimulation.models import Dataset


def mainThread():
    pass

def start():
    
    '''thread = Thread(target = mainThread)
    thread.start()
    thread.join()
    print('.thread finished', end='')
    '''
    data={
        Dataset.objects.all().filter(place="1"),
        Dataset.objects.all().filter(place="2"),
        Dataset.objects.all().filter(place="3"),
        Dataset.objects.all().filter(place="4"),
        Dataset.objects.all().filter(place="5")
        }

    result = []
    for d in data:
        print(d)
