# Event Bus
전역적인 구독 기반의 이벤트 시스템입니다. `IDisposable`을 통한 구독자 객체를 받음으로써 구독시 람다식을 넣기도 용이하고 구독 해제의 간편을 위해 핸들 인터페이스를 추가했고 스레드 안정성을 고려했습니다.

<br>
<br>
<br>

## 🔧Usage

#### 구독 및 해제
```cs
// 구독
IEventSubscription sub = EventBus.Subscribe<PlayerJumpEvent>(OnPlayerJump);

// 구독 해제
sub.Dispose();
```

<br>
<br>
<br>


#### 이벤트 실행
```cs

EventBus.Publish(new ScoreChangedEvent { NewScore = 100 });

```
