# Event Bus
전역적인 구독 기반의 이벤트 시스템입니다. `IDisposable`을 통해 구독 해제의 간편과 null 에러 방지를 위해 핸들 인터페이스를 추가했고 스레드 안정성을 고려했습니다.

<br>
<br>
<br>

## 🔧Usage

### 구독 및 해제
```cs
// 구독
ISubscription sub = EventBus.Subscribe<PlayerJumpEvent>(OnPlayerJump);

// 구독 해제
sub.Dispose();
```

<br>
<br>
<br>


### 이벤트 실행
```cs

EventBus.Publish(new ScoreChangedEvent { NewScore = 100 });

```
